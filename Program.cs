using System;

namespace Restaurant_ConsoleApp__Project_using_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcom to Our Resturant :) ");
            Console.WriteLine("***************************");


            // we mak New object from the Sign class That contains the register function 
            Sign Sign = new Sign();

            
            bool validInput = false;


            while (!validInput)
            {

                Console.WriteLine("Do You Want To ((1)SignUp Or (2)SignIn)");
                // we make variable that contains the user input and convert this input to int 
                // and make a bool variable to check the user input is int (true / false)
                int userType;
                bool isNumeric = int.TryParse(Console.ReadLine(), out userType);

                // this variable will containe the type of sign
                // that will send to the register function as a params
                string signType;

                // condition that check the input is int and
                // the value of this input is between the valid case Numbers
                if (isNumeric && (userType == 1 || userType == 2))
                {

                    switch (userType)
                    {
                        case 1:
                            // Set the value of SignType = SignUp 
                            // to send it to the register function
                            signType = "SignUp";
                            Sign.Register(signType);
                            //after the register function done 
                            // we change the value of validInput to out fron the while loop
                            validInput = true;
                            break;

                        case 2:
                            // Set the value of SignType = SignIn 
                            // to send it to the register function
                            signType = "SignIn";
                            Sign.Register(signType);

                            //after the register function done 
                            // we change the value of validInput to out fron the while loop
                            validInput = true;

                            break;

                    }

                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number (1 or 2).");
                }

            }

            // The Menus that appear to the Type Of Users
            if (CurrentUser.User != null)
            {
                bool validUser = false;
                Console.WriteLine("***********************************");
                Console.WriteLine($"hello {CurrentUser.User.Name} Choose What do you want to do ?");


                // we get the type Of user that in the memory from CurrentUser static class
                string Type = CurrentUser.TypeOfUser;


                // we check the typr of current user that comes from the memory to give the access to the user
                if (Type == "Customer")
                {

                   
                    while (!validUser && validInput == true)
                    {
                        Console.WriteLine("***************************");
                        Console.WriteLine("1) View Menu and Make Order");
                        Console.WriteLine("2) Book a table");
                        Console.WriteLine("3) View My History");
                        Console.WriteLine("4) Log out");
                        Console.WriteLine("***************************");


                        int MenuOptions;
                        bool isnumeric = int.TryParse(Console.ReadLine(), out MenuOptions);


                        if (isnumeric && (MenuOptions == 1 || MenuOptions == 2 || MenuOptions == 3 || MenuOptions == 4 ))
                        {

                            switch (MenuOptions)
                            {

                                case 1:
                                    Console.WriteLine("View Menu and Make Order Options");
                                    break;
                                case 2:
                                    Console.WriteLine("Book a table Options");
                                    break;
                                case 3:
                                    Console.WriteLine("View My History");
                                    break;
                                case 4:
                                    // this is logout function that remove the current user data from the memory
                                    CurrentUser.ClearCurrentUser();
                                    Console.WriteLine("You Loged Out Successfully");
                                    validInput = false;
                                    validUser = true;
                                    break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please enter a valid number.");
                        }

                    }
                } 
                else if (Type == "Waiter") 
                {
                    Console.WriteLine("Waiter Menu");

                } 
                else if (Type == "Chef")
                {
                    Console.WriteLine("Chef Menu");
                } 
            }
            else
            {
                Console.WriteLine("Admin Menu");
            }

        }
    }
}
