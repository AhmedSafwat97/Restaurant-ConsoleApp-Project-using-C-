using System;
using System.Xml;


public class User
{
    public int Id { get; private set; } 
    public string Email { get; set; }
    public string Password { get; set; }


    public User()
    {
        Id = 1;
    }


    public void Register ()
    {

        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Enter Number Of user type ((1)Customer, (2)Admin, (3)Worker):");
            int userType;
            bool isNumeric = int.TryParse(Console.ReadLine(), out userType);

            if (isNumeric && (userType == 1 || userType == 2 || userType == 3))
            {
                switch (userType)
                {
                    case 1:
                        Customer C = new Customer();
                        GetEmailAndPassword(C);
                        Console.WriteLine("You are signed up now as a customer");
                        validInput = true;
                        break;
                    case 2:
                        Admin A = new Admin();
                        GetEmailAndPassword(A);
                        Console.WriteLine("You are signed up now as an admin");
                        validInput = true;
                        break;
                    case 3:
                        Worker w = new Worker();
                        bool validWorkerType = false;
                        while (!validWorkerType)
                        {
                            Console.WriteLine("Is the worker a Waiter or a Chef? ((1)Waiter/ (2)Chef):");
                            int workerType;
                            bool isNumericWorkerType = int.TryParse(Console.ReadLine(), out workerType);

                            if (isNumericWorkerType && (workerType == 1 || workerType == 2))
                            {
                                switch (workerType)
                                {
                                    case 1:
                                        Waiter Waiter1 = new Waiter();
                                        GetEmailAndPassword(Waiter1);
                                        Console.WriteLine("You are signed up now as a waiter");
                                        validWorkerType = true;
                                        break;

                                    case 2:
                                        Chef Chef1 = new Chef();
                                        GetEmailAndPassword(Chef1);
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


    void GetEmailAndPassword(User user)
    {
        Console.WriteLine("Enter Email:");
        user.Email = Console.ReadLine();

        Console.WriteLine("Enter Password:");
        user.Password = Console.ReadLine();
    }




}
  