using System.Text.RegularExpressions;

namespace Calculator.m1chael888
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            Console.WriteLine("Console Calculator in C#");
            Console.WriteLine("------------------------\n");

            while (!done)
            {
                // declare and initialize variables
                string? input1 = "";
                string? input2 = "";
                double result = 0;

                // capture user input
                Console.Write("Type a number, and then press Enter: ");
                input1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(input1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    input1 = Console.ReadLine();
                }

                // capture second user input
                Console.Write("Type another number, and then press Enter: ");
                input2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(input2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    input2 = Console.ReadLine();
                }

                // ask user to choose an operator
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your choice: ");

                string? op = Console.ReadLine();

                // validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input");
                }
                else
                {
                    try
                    {
                        result = Calc.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a math error\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") done = true;
                Console.WriteLine("\n");
            }
            return;
        }
    }

    class Calc
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            // handle operation passed as op
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    // loop untill user enters a non zero number
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
