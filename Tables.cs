using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;
using Restaurant_ConsoleApp__Project_using_C_;

public class Tables
{
    [JsonProperty]
    public int TableID { get; private set; }

    [JsonProperty]
    public int TableSize { get; private set; }

    public bool ReservedOrNot { get; set; }

    public static string UnifiedFilePath = @"E:\ITI FullStack .Net Courses\C#\Project\Restaurant ConsoleApp  Project using C#\Json Files\Tables.json";

    public DateTime? ReservedFrom { get; set; }

    public DateTime? ReservedTo { get; set; }

    public User? ReservedBy { get; set; }

    public static List<Tables> AllTables;

    //static List<Tables> TablesList;


    //public static List<Tables> SharedListOfReservedTables = new List<Tables>();

    //public static List<Tables> ReservedTables
    //{
    //    get { return SharedListOfReservedTables; }
    //}

    //public static List<Tables> ExistingTables
    //{
    //    get { return AllTables; }
    //}

    static Tables()
    {
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(UnifiedFilePath));
        //SharedListOfReservedTables = new List<Tables>();
    }

    //public Tables(int ID, int size)
    //{
    //    TableID = ID;
    //    TableSize = size;
    //    ReservedOrNot = false;
    //    ReservedFrom = null;
    //    ReservedTo = null;
    //    ReservedBy = null;
    //}

    //private static void SetAllTablesData()
    //{
    //    string TablesJSON = File.ReadAllText(ConfigurationManager.AppSettings["JsonFilesPath"] + "Tables.json");
    //    AllTables = JsonConvert.DeserializeObject<List<Tables>>(TablesJSON);
    //}




    public static void UpdateTablesData()
    {
        //SetAllTablesData();

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

    public static void ShowAvailableTables()
    {
        UpdateTablesData();

        List<Tables> AvailableTables = new List<Tables>();

        if (File.Exists(UnifiedFilePath))
        {
            //SetAllTablesData();

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
            {
                Console.WriteLine("We are sorry but there're no available tables at the moment");
            }
        }
        else
        {
            Console.WriteLine("Sorry we encountered an error while handling the data on our side!");
        }
    }

    public static void ReserveTable()
    {
        Console.WriteLine("please enter the ID of the table you want to reserve (or type 'exit' to exit): ");
        string userInput = Console.ReadLine();

        if (userInput.ToLower() == "exit")
        {
            Console.Write("You chose to exit the reservation process! Goodbye!");
            return;
        }

        if (int.TryParse(userInput, out int ChosenTableID))
        {


            //UpdateTablesData();

            Tables ChosenTable = AllTables.FirstOrDefault(t => t.TableID == ChosenTableID);

            if (ChosenTable != null)
            {
                if (ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo > DateTime.Now)
                {
                    Console.WriteLine($"We are afraid this table is already reserved until {ChosenTable.ReservedTo}");
                }
                else
                {
                    ChosenTable.ReservedOrNot = false;
                    ChosenTable.ReservedFrom = null;
                    ChosenTable.ReservedTo = null;
                    ChosenTable.ReservedBy = null;

                    DateTime start;
                    DateTime end;

                    bool DateTimeInputStatus = false;

                    while (DateTimeInputStatus == false)
                    {

                        Console.WriteLine("Enter the start date (yyyy-MM-dd):");
                        string startDateInput = Console.ReadLine();
                        Console.WriteLine("Enter the start time (HH:mm):");
                        string startTimeInput = Console.ReadLine();
                        Console.WriteLine("Enter the end time (HH:mm):");
                        string endTimeInput = Console.ReadLine();



                        //if (DateTime.TryParseExact(Console.ReadLine(), "Write The time that you want to start as yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out start)
                        //&& DateTime.TryParseExact(Console.ReadLine(), "Write The time that you want to end as  yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))


                        if (DateTime.TryParseExact(startDateInput + " " + startTimeInput, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out start)
                            && DateTime.TryParseExact(startDateInput + " " + endTimeInput, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
                        {
                            if (start < end)
                            {
                                ChosenTable.ReservedFrom = start;
                                ChosenTable.ReservedTo = end;
                                ChosenTable.ReservedOrNot = true;
                                ChosenTable.ReservedBy = CurrentUser.User;

                                //SharedListOfReservedTables.Add(ChosenTable);

                                Console.WriteLine($"table number: {ChosenTableID} has been reserved by {CurrentUser.User.Name}: from {ChosenTable.ReservedFrom}  until {ChosenTable.ReservedTo}");

                                Tables? TableToUpdate = AllTables.FirstOrDefault(t => t.TableID == ChosenTable.TableID);
                                if (TableToUpdate != null)
                                {
                                    TableToUpdate.ReservedOrNot = ChosenTable.ReservedOrNot;
                                    TableToUpdate.ReservedFrom = ChosenTable.ReservedFrom;
                                    TableToUpdate.ReservedTo = ChosenTable.ReservedTo;
                                }

                                string UpdatedJson = JsonConvert.SerializeObject(AllTables, Formatting.Indented );
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

                                //SharedListOfReservedTables.Add(ChosenTable);

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
                        {
                            Console.WriteLine("the data and time data you entered is invalid! Reservation failed! Try again!");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("the table you chose does not exist! Reservation failed!");
            }
        }
        else
        {
            Console.WriteLine("your ID input is invaild because it's not a valid integer, reservation failed!");
        }
    }
}
