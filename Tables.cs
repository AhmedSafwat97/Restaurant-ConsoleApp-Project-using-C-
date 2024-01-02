using System;
using Newtonsoft.Json;
public class Tables
{
    public int tableID { get; set; }
    public int tableSize { get; set; }
    public bool reservedOrNot { get; set; }

    public DateTime? reservedFrom { get; set; }

    public DateTime? reservedTo { get; set; }

    private static List<Tables> sharedListOfReservedTables = new List<Tables>();

    public static List<Tables> reservedTables
    {
        get { return sharedListOfReservedTables; }
    }

    static Tables()
	{
        sharedListOfReservedTables = new List<Tables>();
    }

    public Tables(int ID, int size)
    {
        tableID = ID;
        tableSize = size;
        reservedOrNot = false;
        reservedFrom = null;
        reservedTo = null;
    }

    /*
    public static List<Tables> generateData()
    {
        List<Tables> myTables = new List<Tables>();

        for (int i = 1; i <= 10; i++)
        {
            myTables.Add(new Tables(i, 4));
        }

        for (int i = 11; i <= 20; i++)
        {
            myTables.Add(new Tables(i, 8));
        }
        
        return myTables;
    }
    */



    public void reserveATable(Tables chosenTable)
    {
        if (chosenTable.reservedOrNot)
        {
            Console.WriteLine("We are afraid this table is already taken!");
        }
        else
        {
            //flag to keep track of reservation 
            DateTime start;
            DateTime end;
            Console.WriteLine("please specify the time interval you want to reserve the table for: ");
            if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
            {
                chosenTable.reservedFrom = start;
                chosenTable.reservedTo = end;
                chosenTable.reservedOrNot = true;
                sharedListOfReservedTables.Add(chosenTable);
            }
            else
                Console.WriteLine("Please enter a valid date and time!");
        }
    }
}
