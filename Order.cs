using System;
using Newtonsoft.Json;
using System.Configuration;
using Restaurant_ConsoleApp__Project_using_C_;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Order : Menu
{
    public string getitem { get; set; }

    Menu Items;

    public static void order()
    {
        List<string> ordername = new List<string>();
        ordername.Add(CurrentUser.User.Name);
        ordername.Add(CurrentUser.User.Email);
        ordername.Add(CurrentUser.User.Address);


        foreach (string name in ordername)
        {
            Console.WriteLine(name);
        }


    }

    public void GetMealNameId()
    {
        string menuJsonFilePath = File.ReadAllText(ConfigurationManager.AppSettings["JsonFilesPath"] + "menu.json");
        Menulist = JsonConvert.DeserializeObject<List<Menu>>(menuJsonFilePath);

        Console.WriteLine("\n enter the meal id : \n");
        if (Menulist.Count > 0)
        {
            int IdFromUser = int.Parse(Console.ReadLine());
            Items = Menulist[IdFromUser - 1];
            getitem = Items.Description;
            Console.WriteLine(string.Format("\n Meal Name: {0, -20} \t Price: {1, -10} \n Description: {2, -30}", Items.Item, Items.Price, Items.Description));
        }
        else
        {
            Console.WriteLine("The menu is empty.");
        }

        //int OrderMealjason = Path.Combine(order ,(Items.Item, Items.Price, Items.Description));
    }

    /*public void OrderMeal()
    {
        GetMealNameId();
        List<T> data = new List<T>();
        data = JsonConvert.serializeObject<List<T>>(order) ?? new List<T>();


        string OrderMealjason = Path.Combine(order );
    }*/

}
