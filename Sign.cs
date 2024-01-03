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


namespace Restaurant_ConsoleApp__Project_using_C_
{
    internal class Sign : User
    {
        // ______________________________________
        // For Sign UP Logic
        void GetEmailAndPassword(User user)
        {
            Console.WriteLine("Enter Your Name:");
            user.Name = Console.ReadLine();

            Console.WriteLine("Enter Your Email:");
            user.Email = Console.ReadLine();

            Console.WriteLine("Enter Your Password:");
            user.Password = Console.ReadLine();

            Console.WriteLine("Enter Your Phone:");
            user.Phone = Console.ReadLine();

            Console.WriteLine("Enter Your Address:");
            user.Address = Console.ReadLine();
        }


        public void Register(string TypeOfSign)
        {

            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("Enter Number Of user type ((1)Customer Or (2)Worker):");
                int userType;
                bool isNumeric = int.TryParse(Console.ReadLine(), out userType);

                if (isNumeric && (userType == 1 || userType == 2 ))
                {
                    switch (userType)
                    {
                        case 1:
                            Customer C = new Customer();
                            GetEmailAndPassword(C);
                            SaveUserData(C, "Customer.json");
                            Console.WriteLine("You are signed up now as a customer");
                            validInput = true;
                            break;

                        case 2:
                            Worker w = new Worker();
                            bool validWorkerType = false;
                            while (!validWorkerType)
                            {
                                Console.WriteLine("You are Waiter or a Chef? ((1)Waiter/ (2)Chef):");
                                int workerType;
                                bool isNumericWorkerType = int.TryParse(Console.ReadLine(), out workerType);

                                if (isNumericWorkerType && (workerType == 1 || workerType == 2))
                                {
                                    switch (workerType)
                                    {
                                        case 1:
                                            Waiter Waiter1 = new Waiter();
                                            GetEmailAndPassword(Waiter1);
                                            SaveUserData(Waiter1 , "Waiter.json");
                                            Console.WriteLine("You are signed up now as a waiter");
                                            validWorkerType = true;
                                            break;

                                        case 2:
                                            Chef Chef1 = new Chef();
                                            GetEmailAndPassword(Chef1);
                                            SaveUserData(Chef1, "Chef.json");
                                            Console.WriteLine("You are signed up now as a chef");
                                            validWorkerType = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice. Please enter a valid number (1 or 2).");
                                }
                            }
                            validInput = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number (1, 2, or 3).");
                }
            }
        }


        public void SaveUserData<T>(T userData , string fileName) where T : User
        {

            string directoryPath = @"E:\ITI FullStack .Net Courses\C#\Project\Restaurant ConsoleApp  Project using C#\Json Files";
            string filePath = Path.Combine(directoryPath, fileName);

            List<T> data = new List<T>();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the existing data and deserialize it
                string existingJson = File.ReadAllText(filePath);
                data = JsonConvert.DeserializeObject<List<T>>(existingJson) ?? new List<T>();
            }

            // Check if the email already exists
            if (!data.Any(user => user.Email.Equals(userData.Email, StringComparison.OrdinalIgnoreCase)))
            {
                // Determine the next ID
                int nextId = data.Count == 0 ? 1 : data.Max(user => user.Id) + 1;
                userData.Id = nextId;

                // Add the new user data to the list
                data.Add(userData);

                // Serialize the updated list and write it back to the file
                string updatedJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            } else
            {

                Console.WriteLine("This email is already in use. Please try a different email.");
            }



        }



    }
}
