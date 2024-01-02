using System;
using Newtonsoft.Json;
public class Tables
{
    public int tableID { get; set; }
    public int tableSize { get; set; }
    public bool reservedOrNot { get; set; }

    public DateTime reservedFrom { get; set; }  

    public DateTime reservedTo { get; set; }

    private static List<Tables> sharedListOfReservedTables = new List<Tables>();

    public static List<Tables> reservedTables
    {
        get { return reservedTables; }
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
    }

    public void reserveATable(Tables chosenTable)
    {
        if (chosenTable.reservedOrNot)
        {
            Console.WriteLine("We are afraid this table is already taken!");
        }
        else
        {
            Console.WriteLine("please enter when you want to reserve this table");
            DateTime beginReservation = DateTime.TryParse(Console.ReadLine(), );
            chosenTable.reservedOrNot = true;
            sharedListOfReservedTables.Add(this);
        }
    }
}
