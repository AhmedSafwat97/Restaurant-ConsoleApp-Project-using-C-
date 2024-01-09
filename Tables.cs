using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Globalization;

public class Tables
{
    public int TableID { get; private set; }

    public int TableSize { get; private set; }

    public bool ReservedOrNot { get; set; }

    public static string UnifiedFilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";

    public DateTime? ReservedFrom { get; set; }

    public DateTime? ReservedTo { get; set; }

    public User? ReservedBy { get; set; }

    private static List<Tables> AllTables = new List<Tables>();

    private static List<Tables> SharedListOfReservedTables = new List<Tables>();

    public static List<Tables> ReservedTables
    {
        get { return SharedListOfReservedTables; }
    }

    public static List<Tables> ExistingTables
    {
        get { return AllTables; }
    }

    static Tables()
    {
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(UnifiedFilePath));
        SharedListOfReservedTables = new List<Tables>();
    }

    public Tables(int ID, int size)
    {
        TableID = ID;
        TableSize = size;
        ReservedOrNot = false;
        ReservedFrom = null;
        ReservedTo = null;
        ReservedBy = null;  
    }

    private static void SetAllTablesData()
    {
        string AllTablesJSON = File.ReadAllText(UnifiedFilePath);
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(UnifiedFilePath));
    }

    public static void GenerateSampleData()
    {
        List<Tables> tables = new List<Tables>();

        for (int i = 1; i <= 20; i++)
        {
            int TableSize = (i <= 10) ? 4 : 8;

            Tables table = new Tables(i, TableSize);
            tables.Add(table);
        }

        string json = JsonConvert.SerializeObject(tables);

        File.WriteAllText(UnifiedFilePath, json);
    }

    public static void UpdateTablesData()
    {
        SetAllTablesData();

        foreach (var table in AllTables)
        {
            if (table.ReservedTo < DateTime.Now)
            {
                table.ReservedOrNot = false;
                table.ReservedFrom = null;
                table.ReservedTo = null;
                table.ReservedBy = null;
            }
        }
    }

    //
    public static void ShowAvailableTables()
    {
        UpdateTablesData();

        List<Tables> AvailableTables = new List<Tables>();

        if (File.Exists(UnifiedFilePath))
        {
            SetAllTablesData();

            AvailableTables = AllTables.Where(table => table.ReservedOrNot == false).ToList();

            if (AvailableTables.Count != 0)
            {
                Console.WriteLine("The following list contains all the available tables and their sizes:");
                foreach (var table in AvailableTables)
                {
                    Console.WriteLine($"Table ID: {table.TableID}, Size: {table.TableSize}");
                }
            }

            else
                Console.WriteLine("We are sorry but there're no available tables at the moment");

        }
        else
            Console.WriteLine("Sorry we encountered an error while handling the data on our side!");
    }


    public static void ReserveTable()
    {

        int ChosenTableID;
        Console.WriteLine("please enter the ID of the table you want to reserve (or type 'exit' to exit): ");
        string userInput = Console.ReadLine();
        //reading user input as a string so we can take both an ID or exit choice

        if (userInput.ToLower() == "exit")
        {
            Console.Write("You chose to exit the reservation process! Goodbye!");
            return;
            //terminate the function immediately
        }

        if (int.TryParse(userInput, out ChosenTableID))//now we're sure the input is a vaild int
        {
            //making sure we're up-to-date with latest version of our JSON file
            UpdateTablesData();

            //check if there's a table with the chosen ID
            Tables? ChosenTable = AllTables.FirstOrDefault(t => t.TableID == ChosenTableID);

            //if it does exit start executing the code
            if (ChosenTable != null)
            {

                if (ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo > DateTime.Now)//chosen table is still reserved
                {
                    Console.WriteLine($"We are afraid this table is already reserved until {ChosenTable.ReservedTo}");
                }

                else //it can be reserved
                {
                    //might have some outdated properties so we reset all of them just-in-case
                    ChosenTable.ReservedOrNot = false;
                    ChosenTable.ReservedFrom = null;
                    ChosenTable.ReservedTo = null;
                    ChosenTable.ReservedBy = null;

                    // out of tryParse cannot assign data to an obj so we can't use ChosenTable.ReservedFrom for example
                    // we need some variables to take values from tryParse
                    DateTime start;
                    DateTime end;

                    Console.WriteLine("please specify the time interval you want to reserve the table for:\n" + "date and time in the format yyyy-MM-dd HH:mm:ss");

                    bool DateTimeInputStatus = false;

                    while (DateTimeInputStatus == false)
                    {
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out start)
                        && DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
                        {
                            if (start < end)
                            {
                                ChosenTable.ReservedFrom = start;
                                ChosenTable.ReservedTo = end;
                                ChosenTable.ReservedOrNot = true;
                                ChosenTable.ReservedBy = CurrentUser.User;

                                //might need separate JSON file
                                SharedListOfReservedTables.Add(ChosenTable);

                                Console.WriteLine($"table number: {ChosenTableID} has been reserved by customer:  until {ChosenTable.ReservedTo}");

                                Tables? TableToUpdate = AllTables.FirstOrDefault(t => t.TableID == ChosenTable.TableID);
                                if (TableToUpdate != null)
                                {
                                    TableToUpdate.ReservedOrNot = ChosenTable.ReservedOrNot;
                                    TableToUpdate.ReservedFrom = ChosenTable.ReservedFrom;
                                    TableToUpdate.ReservedTo = ChosenTable.ReservedTo;
                                }

                                string UpdatedJson = JsonConvert.SerializeObject(AllTables, Formatting.Indented);
                                File.WriteAllText(UnifiedFilePath, UpdatedJson);

                                DateTimeInputStatus = true;
                            }

                            else if (start > end)
                            {
                                Console.WriteLine("we think that you confused the starting time with ending time and we'll handle it");
                                ChosenTable.ReservedFrom = end;
                                ChosenTable.ReservedTo = start;
                                ChosenTable.ReservedOrNot = true;
                                ChosenTable.ReservedBy = CurrentUser.User;

                                SharedListOfReservedTables.Add(ChosenTable);

                                Console.WriteLine($"table number: {ChosenTableID} has been reserved by customer:  until {ChosenTable.ReservedTo}");

                                Tables? TableToUpdate = AllTables.FirstOrDefault(t => t.TableID == ChosenTable.TableID);
                                if (TableToUpdate != null)
                                {
                                    TableToUpdate.ReservedOrNot = ChosenTable.ReservedOrNot;
                                    TableToUpdate.ReservedFrom = ChosenTable.ReservedFrom;
                                    TableToUpdate.ReservedTo = ChosenTable.ReservedTo;
                                }

                                string UpdatedJson = JsonConvert.SerializeObject(AllTables, Formatting.Indented);
                                File.WriteAllText(UnifiedFilePath, UpdatedJson);

                                DateTimeInputStatus = true;
                            }

                            else if (start == end)
                            {
                                Console.WriteLine("the start and end time cannot be equal, please try again");
                            }
                        }

                        else
                            Console.WriteLine("the data and time data you entered is invalid! Reservation failed! Try again!");
                    }

                }

            }

            else
                Console.WriteLine("the table you chose does not exist! Reservation failed!");
        }

        else
        {
            Console.WriteLine("your ID input is invaild because it's not a valid integer, reservation failed!");
        }

    }
}
