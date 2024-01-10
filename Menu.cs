using Newtonsoft.Json;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Restaurant_ConsoleApp__Project_using_C_
{




    
    // create class menu that contains 4 properties (id,item,price,descreption) and list of menu 
    // using properties to make the program more secure and 
    //enable a class to expose a public way of getting and setting values, while hiding implementation 
    public class Menu
    {

        public int Id { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        // making list of menu called menulist
        public List<Menu> Menulist;

        //List<Menu> Menulist = new List<Menu> { };

        // create a void function to print menu data 
        public void PrintMenu()

        {
            string menuJsonFilePath = File.ReadAllText(ConfigurationManager.AppSettings["JsonFilesPath"] + "menu.json");
            Menulist = JsonConvert.DeserializeObject<List<Menu>>(menuJsonFilePath);


            //string menuJsonFilePath =ConfigurationManager.AppSettings["JsonFilesPath"] + "menu.json";

            //////if (File.Exists(menuJsonFilePath))
            //////{
            //////    // Read the existing data and deserialize it
            //string json = File.ReadAllText(menuJsonFilePath);
            //Menulist = JsonConvert.DeserializeObject<List<Menu>>(json);

            ////}


            //string basePath = ConfigurationManager.AppSettings["JsonFilesPath"];
            //if (!string.IsNullOrEmpty(basePath))
            //{
            //    string menuJsonFilePath = Path.Combine(basePath, "menu.json");

            //    if (File.Exists(menuJsonFilePath))
            //    {
            //        try
            //        {
            //            string json = File.ReadAllText(menuJsonFilePath);
            //            Menulist = JsonConvert.DeserializeObject<List<Menu>>(json) ?? new List<Menu>();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Handle the exception, e.g., log the error or take appropriate action
            //            Console.WriteLine("Error occurred during JSON deserialization: " + ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        // Handle the case where the file doesn't exist
            //        Console.WriteLine("Menu JSON file does not exist.");
            //    }
            //}
            //else
            //{
            //    // Handle the case where the base path is not specified
            //    Console.WriteLine("JsonFilesPath is not specified in the configuration.");
            //}








            //string json = File.ReadAllText(menuJsonFilePath);
            //Menulist = JsonConvert.DeserializeObject<List<Menu>>(json) ?? new List<Menu>();
            // take data from jason file by readalltext and set the path of jsonfile and put them in variable called "json" with string data type



            // put the data in the menu list that called menulist by making Deserializition of the list menu from variable json that take the path of json file
            ///

            //  start a new line by (\n)
            Console.WriteLine("Menu \n");

            Console.WriteLine("------------------------------------------------------");
            /* 1) print the title of the table i made with fixed spaces by using string.format method that make Control alignment
               2)using (-) ---> to make left-aligned field.
               3) Note : By default strings are right-aligned  so i  make left-aligned to make spaces between fields 
               4) meaning of   {0,-3}     0---> first field     -3 ----> to define a 3-character left-aligned field.
               5) the formatted string will have the first argument (Id) left-aligned in a 3-character wide space,
                the second argument (Meal Name) left-aligned in a 30-character wide space,
                and the third argument (Price) left-aligned in a 10-character wide space. 
                 The \t is a tab character for spacing.
              */
            Console.WriteLine(string.Format(" {0, -3}  {1, -30} \t {2, -10} ", "Id", " Meal Name ", "Price "));
            Console.WriteLine("------------------------------------------------------");

            // making foreach to loop on each item in  the menu list that called menulist
            foreach (var item in Menulist)
            {

                // with string formating ---> print ( the item's ID, name, and price)

                Console.WriteLine(string.Format(" {0, -5} Meal Name: {1, -20} \t Price: {2, -10} ", item.Id, item.Item, item.Price));


            }

        }

        /*
                making a function called "GetMealNameId" to get from user the id of meal name 
                after the user see the menu from the "PrintMenu" function and print all the data of menu 
                includes (id of meal , name of meal ,  price of the meal, description of the meal) 

                */
        /*public void GetMealNameId()
        {



            Console.WriteLine("\n enter the meal id : \n");

            // using if to make validition to sure that the menu list have items and not empty before the user choose from menu
            // . count --->is a property that  Count the total number of elements in the List
            // the condition ----> if the total number of elements in the menulist is more than zero (have at least an item)

            if (Menulist.Count > 0)
            {
                // take from the user the id of the meal name 
                // int . parse ---->to parse a string variable called IdFromUser to an integer
                int IdFromUser = int.Parse(Console.ReadLine());
                /*
                 * (menu items)---> variable that assigned the menu item from the Menulist 
                square brackets to access the index of the Menulist. 
                subtracting 1 from IdFromUser because list indices start from 0 index
                that means the first element is at index 0 so 
                if the user but the id = 1 ---> [1-1]=0 that display the first item that carry the 0 index

                 //


                Menu Items = Menulist[IdFromUser - 1];

                // with string formating ---> print ( the item's name, and price,Description) that the user choose by the id

                Console.WriteLine(string.Format("\n Meal Name: {0, -20} \t Price: {1, -10} \n Description: {2, -30}", Items.Item, Items.Price, Items.Description));
            }

            // if the list is empty print ("The menu is empty.")
            else
            {
                Console.WriteLine("The menu is empty.");
            }
        }*/
    }
}



























