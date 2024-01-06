using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Tables
{
    public int TableID { get; set; }

    public int TableSize { get; set; }

    public bool ReservedOrNot { get; set; }

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



    public static void GenerateSampleData()
    {
        List<Tables> tables = new List<Tables>();

        for (int i = 1; i <= 20; i++)
        {
            int TableSize = (i <= 10) ? 4 : 7;

            Tables table = new Tables(i, TableSize);
            tables.Add(table);
        }

        string json = JsonConvert.SerializeObject(tables);

        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        File.WriteAllText(FilePath, json);
    }


    public static void ShowAvailableTables ()
    {
        List<Tables> AvailableTables = new List<Tables>();
        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(FilePath));
        AvailableTables = AllTables.Where(table => !table.ReservedOrNot).ToList();

        Console.WriteLine("The following list contains all the available tables and their sizes:");
        foreach (var table in AvailableTables)
        {
            Console.WriteLine($"Table ID: {table.TableID}, Size: {table.TableSize}");
        }
    }

    public static void ReserveATable(Tables ChosenTable /*,Customer CurrentUser*/)
    {
        if (ChosenTable.ReservedOrNot==true&& ChosenTable.ReservedTo> DateTime.Now)
        {
            Console.WriteLine($"We are afraid this table is already reserved until {ChosenTable.ReservedTo}");
        }

        else //if ((ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo < DateTime.Now)||ChosenTable.ReservedOrNot==false)
        {
            ChosenTable.ReservedOrNot = false;
            ChosenTable.ReservedFrom = null;
            ChosenTable.ReservedTo = null;
            //ChosenTable.ReservedBy = null;

            DateTime start;
            DateTime end;

            Console.WriteLine("please specify the time interval you want to reserve the table for: ");
            if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
            {
                ChosenTable.ReservedFrom = start;
                ChosenTable.ReservedTo = end;
                ChosenTable.ReservedOrNot = true;
                //ChosenTable.ReservedBy = CurrentUser;
                SharedListOfReservedTables.Add(ChosenTable);

                Console.WriteLine($"table number: {ChosenTable.TableID} has been reserved by customer:  until {ChosenTable.ReservedTo}");

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
                File.WriteAllText(FilePath, UpdatedJson);
            }

            else
                Console.WriteLine("The data you entered is invalid! Reservation Failed!");
        }

    }
}

