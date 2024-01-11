using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Restaurant_ConsoleApp__Project_using_C_
{
    public class Order : Menu
    {
        public int OrderId { get; set; }
        public User OrderedBy { get; set; }
        public List<Menu> OrderedItems { get; set; }
        public double TotalPrice { get; set; }

        public Order()
        {
            OrderedItems = new List<Menu>();
            OrderedBy = CurrentUser.User;
            TotalPrice = 0;
        }
        public void AddItemToOrder(Menu itemToAdd)
        {
            var existingItem = OrderedItems.FirstOrDefault(item => item.Id == itemToAdd.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                OrderedItems.Add(itemToAdd);
            }

            TotalPrice = OrderedItems.Sum(item => item.Price * item.Quantity);
        }

        public void CreateOrder(Menu menu) // Pass Menu object to access Menulist
        {
            Console.WriteLine("Please select the items to add to your order. Enter 0 to finish.");

            int itemId;
            do
            {
                Console.Write("Enter item ID (0 to finish): ");
                if (int.TryParse(Console.ReadLine(), out itemId) && itemId > 0)
                {
                    Menu item = menu.GetMealNameId(itemId);
                    if (item != null)
                    {
                        AddItemToOrder(item);
                        Console.WriteLine("Item added to order.");
                    }
                    else
                    {
                        Console.WriteLine("Item not found.");
                    }
                }
            } while (itemId != 0);

            Console.WriteLine("Order created with total price: " + TotalPrice);
        }

        // Method to save the order to a JSON file
        public void SaveOrder()
        {
            string directoryPath = ConfigurationManager.AppSettings["JsonFilesPath"];
            string filePath = Path.Combine(directoryPath, "Orders.json");
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Order saved to JSON file.");
        }
    }
}