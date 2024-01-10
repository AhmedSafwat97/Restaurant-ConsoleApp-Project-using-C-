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
using System.Configuration;


namespace Restaurant_ConsoleApp__Project_using_C_
{
    internal class Sign : User
    {

        // ______________________________________
        // For Sign UP Logic


        // for get the sign up data from the user
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

        // to get the Sign in data from the user if the use is type of admin
        void SignWithEmailAndPassword(User user)
        {
            Console.WriteLine("Enter Your Email:");
            user.Email = Console.ReadLine();

            Console.WriteLine("Enter Your Password:");
            user.Password = Console.ReadLine();

        }

        /// The defult value That we put in the wilr loop 
        bool validInput = false;

        //this function  contain the tow conditions of type of sign 
        public void HandleUserRegistrationOrLogin<T>( string typeOfSign, string fileName , string TypeOfUser) where T : User, new()
        {

            T user = new T();

            if (typeOfSign == "SignUp")
            {
                GetEmailAndPassword(user);
                SaveUserData(user, fileName);
                if (validInput == true)
                {
                    Console.WriteLine($"You are signed up now as a {typeof(T).Name.ToLower()}.") ;
                    CurrentUser.SetCurrentUser(user , TypeOfUser);
                }
            }
            else if (typeOfSign == "SignIn")
            {
                user = GetUserByEmailAndPassword<T>(fileName);
                if (user != null)
                {
                    //Console.WriteLine($"Welcome back, {user.Name}!");
                    CurrentUser.SetCurrentUser(user , TypeOfUser);
                    validInput = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid operation type.");
            }
        }


        // the main function for Sign 
        public void Register(string TypeOfSign)
        {

            while (!validInput && (TypeOfSign == "SignUp" || TypeOfSign == "SignIn"))
            {
                if (TypeOfSign == "SignIn")
                {
                    Console.WriteLine("****************************************************************");
                    Console.WriteLine("Enter Number Of user type ((1)Customer Or (2)Worker) , (3)Admin:");
                    Console.WriteLine("****************************************************************");

                }
                else if (TypeOfSign == "SignUp")
                {
                    Console.WriteLine("****************************************************");
                    Console.WriteLine("Enter Number Of user type ((1)Customer , (2)Worker):");
                    Console.WriteLine("****************************************************");

                }


                int userType;
                bool isNumeric = int.TryParse(Console.ReadLine(), out userType);

                if (isNumeric && (userType == 1 || userType == 2 || userType == 3))
                {
                    switch (userType)
                    {

                        case 1:
                            // we make object from the type of user classes that inherit from the user
                            Customer C = new Customer();

                            //this function take the class object and take the type of sign that comes from the 
                            //program file as a params and the json file name and the type of usr name that we use it give the access
                            HandleUserRegistrationOrLogin<Customer>(TypeOfSign, "Customer.json" , "Customer");
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
                                            HandleUserRegistrationOrLogin<Waiter>(TypeOfSign, "Waiter.json" , "Waiter");
                                            validWorkerType = true;
                                            break;

                                        case 2:
                                            Chef Chef1 = new Chef();
                                            HandleUserRegistrationOrLogin<Chef>(TypeOfSign, "Chef.json", "Chef");
                                            validWorkerType = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice. Please enter a valid number (1 or 2).");
                                }
                            }
                            break;
                        case 3 :
                            //The Admin Type aprrear Only when the typr of user is signin
                            if (TypeOfSign == "SignIn")
                            {
                                Thread t = new Thread(() =>
                                {
                                    var admin = Admin.Iadmin("adaa", "111");
                                    SignWithEmailAndPassword(admin);
                                    if (admin.Email == "aaa" && admin.Password == "111")
                                    {
                                        Console.WriteLine("You are signed in now as an admin");
                                        validInput = true;
                                    }
                                    else
                                        Console.WriteLine("Invalid Email or Password");
                                    
                                });
                                t.Start();
                                t.Join();
                            } else
                            {
                                Console.WriteLine("Invalid choice. Please enter a valid number .");
                            }
                                    

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number .");
                }
            }
        }

        // this function used to seve data to the json file and check fot the data using the user input
        //genaric
        public void SaveUserData<T>(T userData , string fileName) where T : User
        {

            string directoryPath = ConfigurationManager.AppSettings["JsonFilesPath"];
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

                validInput = true;

            }
            else
            {

                Console.WriteLine("This email is already in use. Please try a different email.");
                validInput = false;
            }



        }


        // to check for the user Signin data ang check for this ata in the jso file and return the data if it exise
        public T GetUserByEmailAndPassword<T>(string fileName) where T : User, new()
        {
            Console.WriteLine("Enter Your Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Your Password:");
            string password = Console.ReadLine();

            string directoryPath = ConfigurationManager.AppSettings["JsonFilesPath"];

            string filePath = Path.Combine(directoryPath, fileName);

            List<T> data = new List<T>();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the existing data and deserialize it
                string existingJson = File.ReadAllText(filePath);
                data = JsonConvert.DeserializeObject<List<T>>(existingJson) ?? new List<T>();
            }

            // Find the user with the matching email and password
            T user = data.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);

            if (user != null)
            {
                Console.WriteLine($"Welcome back, {user.Name}!");
                return user;
            }
            else
            {

                Console.WriteLine("Invalid email or password. Please try again.");
                validInput = false;
                return null; // Returning null to indicate that no user was found
            }
        }



    }
}
