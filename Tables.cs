using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Tables
{
 //   public int tableID { get; set; }

 //   public int tableSize { get; set; }

 //   public bool reservedOrNot { get; set; }

 //   public DateTime? reservedFrom { get; set; }

 //   public DateTime? reservedTo { get; set; }

 //   public Customer ReservedBy { get; set; }
    
 //   private static List<Tables> sharedListOfReservedTables = new List<Tables>();

 //   public static List<Tables> reservedTables
 //   {
 //       get { return sharedListOfReservedTables; }
 //   }

 //   static Tables()
	//{
 //       sharedListOfReservedTables = new List<Tables>();
 //   }

 //   public Tables(int ID, int size)
 //   {
 //       tableID = ID;
 //       tableSize = size;
 //       reservedOrNot = false;
 //       reservedFrom = null;
 //       reservedTo = null;
 //       ReservedBy = null;  
 //   }


    
 //   public static List<Tables> generateData()
 //   {
 //       List<Tables> myTables = new List<Tables>();

 //       for (int i = 1; i <= 10; i++)
 //       {
 //           myTables.Add(new Tables(i, 4));
 //       }

 //       for (int i = 11; i <= 20; i++)
 //       {
 //           myTables.Add(new Tables(i, 8));
 //       }
        
 //       return myTables;
 //   }
    
 //   //List<Tables> dataToJSON() = Json.Convert


 //   public void reserveATable(Tables chosenTable, Customer CurrentUser)
 //   {
 //       if (chosenTable.reservedOrNot==true&& chosenTable.reservedTo> DateTime.Now)
 //       {
 //           Console.WriteLine($"We are afraid this table is already reserved until {chosenTable.reservedTo}");
 //       }

 //       else //if ((chosenTable.reservedOrNot == true && chosenTable.reservedTo < DateTime.Now)||chosenTable.reservedOrNot==false)
 //       {
 //           chosenTable.reservedOrNot = false;
 //           chosenTable.reservedFrom = null;
 //           chosenTable.reservedTo = null;
 //           chosenTable.ReservedBy = null;

 //           DateTime start;
 //           DateTime end;

 //           Console.WriteLine("please specify the time interval you want to reserve the table for: ");
 //           if (DateTime.TryParse(Console.ReadLine(), out start) && DateTime.TryParse(Console.ReadLine(), out end))
 //           {
 //               chosenTable.reservedFrom = start;
 //               chosenTable.reservedTo = end;
 //               chosenTable.reservedOrNot = true;
 //               chosenTable.ReservedBy = CurrentUser;
 //               sharedListOfReservedTables.Add(chosenTable);

 //               Console.WriteLine($"table number: {chosenTable.tableID} has been reserved by customer: {CurrentUser.Name} until {chosenTable.reservedTo}");
 //           }

 //           else
 //               Console.WriteLine("The data you entered is invalid! Reservation Failed!");
 //       }

       

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
    //}
}

