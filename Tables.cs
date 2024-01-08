public class Tables
{
    public int tableID { get; set; }
    public int TableID { get; set; }

    public int tableSize { get; set; }
    public int TableSize { get; set; }

    public bool reservedOrNot { get; set; }
    public bool ReservedOrNot { get; set; }

    public DateTime? reservedFrom { get; set; }
    public DateTime? ReservedFrom { get; set; }

    public DateTime? reservedTo { get; set; }
    public DateTime? ReservedTo { get; set; }

    //public Customer ReservedBy { get; set; }

    private static List<Tables> AllTables = new List<Tables>();

    private static List<Tables> SharedListOfReservedTables = new List<Tables>();

    public Customer ReservedBy { get; set; }

    private static List<Tables> sharedListOfReservedTables = new List<Tables>();
    public static List<Tables> ReservedTables
    {
        get { return SharedListOfReservedTables; }
    }

    public static List<Tables> reservedTables
    public static List<Tables> ExistingTables
    {
        get { return sharedListOfReservedTables; }
        get { return AllTables; }
    }

    static Tables()
	{
        sharedListOfReservedTables = new List<Tables>();
        string FilePath = @"C:\Users\HP\source\repos\AhmedSafwat97\Restaurant-ConsoleApp-Project-using-C-\Json Files\Tables.json";
        AllTables = JsonConvert.DeserializeObject<List<Tables>>(File.ReadAllText(FilePath));
        SharedListOfReservedTables = new List<Tables>();
    }

    public Tables(int ID, int size)
    {
        tableID = ID;
        tableSize = size;
        reservedOrNot = false;
        reservedFrom = null;
        reservedTo = null;
        ReservedBy = null;  
        TableID = ID;
        TableSize = size;
        ReservedOrNot = false;
        ReservedFrom = null;
        ReservedTo = null;
        //ReservedBy = null;  
    }


    
    public static List<Tables> generateData()

    public static void GenerateSampleData()
    {
        List<Tables> myTables = new List<Tables>();
        List<Tables> tables = new List<Tables>();

        for (int i = 1; i <= 10; i++)
        for (int i = 1; i <= 20; i++)
        {
            myTables.Add(new Tables(i, 4));
            int TableSize = (i <= 10) ? 4 : 7;

            Tables table = new Tables(i, TableSize);
            tables.Add(table);
        }

        for (int i = 11; i <= 20; i++)
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
            myTables.Add(new Tables(i, 8));
            Console.WriteLine($"Table ID: {table.TableID}, Size: {table.TableSize}");
        }

        return myTables;
    }

    //List<Tables> dataToJSON() = Json.Convert


    public void reserveATable(Tables chosenTable, Customer CurrentUser)
    public static void ReserveATable(Tables ChosenTable /*,Customer CurrentUser*/)
    {
        if (chosenTable.reservedOrNot==true&& chosenTable.reservedTo> DateTime.Now)
        if (ChosenTable.ReservedOrNot==true&& ChosenTable.ReservedTo> DateTime.Now)
        {
            Console.WriteLine($"We are afraid this table is already reserved until {chosenTable.reservedTo}");
            Console.WriteLine($"We are afraid this table is already reserved until {ChosenTable.ReservedTo}");
        }

        else //if ((chosenTable.reservedOrNot == true && chosenTable.reservedTo < DateTime.Now)||chosenTable.reservedOrNot==false)
        else //if ((ChosenTable.ReservedOrNot == true && ChosenTable.ReservedTo < DateTime.Now)||ChosenTable.ReservedOrNot==false)
        {
            chosenTable.reservedOrNot = false;
            chosenTable.reservedFrom = null;
            chosenTable.reservedTo = null;
            chosenTable.ReservedBy = null;
            ChosenTable.ReservedOrNot = false;
            ChosenTable.ReservedFrom = null;
            ChosenTable.ReservedTo = null;
            //ChosenTable.ReservedBy = null;

            DateTime start;
            DateTime end;

            Console.WriteLine("please specify the time interval you want to reserve the table for: ");
            if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
            {
                chosenTable.reservedFrom = start;
                chosenTable.reservedTo = end;
                chosenTable.reservedOrNot = true;
                chosenTable.ReservedBy = CurrentUser;
                sharedListOfReservedTables.Add(chosenTable);

                Console.WriteLine($"table number: {chosenTable.tableID} has been reserved by customer: {CurrentUser.Name} until {chosenTable.reservedTo}");
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



        //else
        //{

        //    DateTime start;
        //    DateTime end;
        //    Console.WriteLine("please specify the time interval you want to reserve the table for: ");
        //    if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
        //    {
        //        chosenTable.reservedFrom = start;
        //        chosenTable.reservedTo = end;
        //        chosenTable.reservedOrNot = true;
        //        chosenTable.ReservedBy = CurrentUser;
        //        sharedListOfReservedTables.Add(chosenTable);
        //    }
        //    else
        //        Console.WriteLine("Please enter a valid date and time!");
        //}
    }
}
