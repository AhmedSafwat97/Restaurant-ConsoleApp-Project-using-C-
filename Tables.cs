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

public class Tables
{
    public int TableID { get; private set; }

    public int TableSize { get; private set; }

    public bool ReservedOrNot { get; set; }

    public static string UnifiedFilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";

    public DateTime? ReservedFrom { get; set; }

    public DateTime? ReservedTo { get; set; }

    //public Customer ReservedBy { get; set; }

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
        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(FilePath));
        SharedListOfReservedTables = new List<Tables>();
    }

    public Tables(int ID, int size)
    {
        TableID = ID;
        TableSize = size;
        ReservedOrNot = false;
        ReservedFrom = null;
        ReservedTo = null;
        //ReservedBy = null;  
    }

    private void SetAllTablesData()
    {
        //try creating a global variable for the path (FilePath)
        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        string AllTablesJSON = File.ReadAllText(FilePath);
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(FilePath));
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

        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        File.WriteAllText(FilePath, json);
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
            }
        }
    }

    //
    public static void ShowAvailableTables ()
    {
        UpdateTablesData();

        List<Tables> AvailableTables = new List<Tables>();
        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";

        if (File.Exists(FilePath))
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
                //try to make the user choose the desired available table
            }

            else
                Console.WriteLine("We are sorry but there're no available tables at the moment");
            
        }
        else
            Console.WriteLine("Sorry we encountered an error while handling the data on our side!");
    }


    public static void ReserveATable( int ChosenTableID/*Tables ChosenTable,Customer CurrentUser*/)
    {
    
        //use UpdateTablesData(); just in-case

        // check this code syntax
        Tables ChosenTable = AllTables.FirstOrDefault(t => t.TableID == ChosenTableID);

        if (ChosenTable != null)
        {

            if (ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo > DateTime.Now)
            {
                Console.WriteLine($"We are afraid this table is already reserved until {ChosenTable.ReservedTo}");
            }

            else //if ((ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo < DateTime.Now)||ChosenTable.ReservedOrNot==false)
            {
                ChosenTable.ReservedOrNot = false;
                ChosenTable.ReservedFrom = null;
                ChosenTable.ReservedTo = null;
                //ChosenTable.ReservedBy = null;

                //check here
                DateTime? start=null;
                DateTime? end=null;

                Console.WriteLine("please specify the time interval you want to reserve the table for:\n"+"date and time in the format yyyy-MM-dd HH:mm:ss");
                // search for DateTime formatting so we don't need the full format
                // DateTime.Now.ToString(); built-in function

                if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
                {
                    //validation start < end

                    ChosenTable.ReservedFrom = start;
                    ChosenTable.ReservedTo = end;
                    ChosenTable.ReservedOrNot = true;
                    //ChosenTable.ReservedBy = CurrentUser;

                    //might need separate JSON file
                    SharedListOfReservedTables.Add(ChosenTable);

                    Console.WriteLine($"table number: {ChosenTableID} has been reserved by customer:  until {ChosenTable.ReservedTo}");

                    //string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
                    //string SerializeThisObject = JsonConvert.SerializeObject(ChosenTable, Formatting.Indented);
                    //File.WriteAllText(FilePath,SerializeThisObject);   

                    //string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
                    //List<Tables> ExistingTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(FilePath));

                    // Find and update the specific table in the list

                    Tables TableToUpdate = AllTables.FirstOrDefault(t => t.TableID == ChosenTable.TableID);
                    if (TableToUpdate != null)
                    {
                        TableToUpdate.ReservedOrNot = ChosenTable.ReservedOrNot;
                        TableToUpdate.ReservedFrom = ChosenTable.ReservedFrom;
                        TableToUpdate.ReservedTo = ChosenTable.ReservedTo;
                    }

                    string UpdatedJson = JsonConvert.SerializeObject(AllTables, Formatting.Indented);
                    //dealing with the same FilePath again? Solve the repeation with a global/unified variable?
                    File.WriteAllText(UnifiedFilePath, UpdatedJson);
                }

                else
                    Console.WriteLine("The data you entered is invalid! Reservation Failed!");
            }

        }
        else
            Console.WriteLine("the table you chose does not exist! Reservation failed!");
    }
}

