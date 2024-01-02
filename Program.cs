namespace Restaurant_ConsoleApp__Project_using_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcom to Our Resturant :) ");

            Sign Sign = new Sign();

            bool validInput = false;

            bool IsSignIn = false;


            while (!validInput) {

                Console.WriteLine("Do You Want To ((1)SignUp Or (2)SignIn)");
                int userType;
                bool isNumeric = int.TryParse(Console.ReadLine(), out userType);

                if (isNumeric && (userType == 1 || userType == 2) ) {
                    
                    switch (userType)
                    {
                        case 1:
                            Sign.Register();
                            validInput = true;
                            break;

                        case 2:
                            Sign.SignIn();
                            validInput = true;
                            IsSignIn = true;
                            Console.WriteLine(IsSignIn);
                            break;

                    }

                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number (1 or 2).");
                }

            }

        }

    }
}
