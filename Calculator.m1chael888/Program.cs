using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram.m1chael888
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();
            var history = new Dictionary<string, double>();
            int count = 0;

            Menu();

            void Menu()
            {
                Console.Clear();
                Console.WriteLine("- Calculator Menu -");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("1 - Proceed to calucation");
                Console.WriteLine("2 - View calculation history");
                Console.WriteLine("3 - Exit program");
                Console.Write("\nEnter the number of your desired menu option: ");

                bool done = false;
                while (!done)
                {
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            done = true;
                            Calculation();
                            break;
                        case "2":
                            done = true;
                            ViewHistory();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Write("Invalid choice. Please enter a valid menu option (1-3): ");
                            break;
                    }
                }
            }

            void ViewHistory()
            {
                Console.Clear();

                Console.WriteLine("- Calculation History -");
                Console.WriteLine("------------------------\n");

                if (history.Count() == 0)
                {
                    Console.WriteLine("You have have not completed any calculations!");
                }
                else
                {
                    int calcNum = 0;
                    foreach (var calculation in history)
                    {
                        calcNum++;
                        Console.WriteLine($"Calculation {calcNum}: {calculation.Key}");
                    }
                }
                Console.WriteLine("------------------------");

                bool done = false;
                while (!done)
                {
                    Console.WriteLine("\n1 - Delete History (This cannot be undone)");
                    Console.WriteLine("2 - Return to menu");
                    Console.WriteLine("3 - Exit program");
                    Console.Write("\nHow would you like to proceed? Choose an option: ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            done = true;
                            history.Clear();
                            ViewHistory();
                            break;
                        case "2":
                            done = true;
                            Menu();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid menu option (1-3");
                            break;
                    }
                    if (Console.ReadLine() == "n") done = true;
                }
            }

            void Calculation()
            {
                Console.Clear();

                bool done = false;
                while (!done)
                {
                    string? input1 = "";
                    string? input2 = "";
                    double result = 0;

                    Console.WriteLine("- Conduct Calculation -");
                    Console.WriteLine("------------------------\n");
                    Console.Write("Type a number, and then press Enter: ");
                    input1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(input1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        input1 = Console.ReadLine();
                    }

                    Console.Write("Type another number, and then press Enter: ");
                    input2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(input2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        input2 = Console.ReadLine();
                    }

                    bool done2 = false;
                    while (!done2)
                    {
                        Console.WriteLine("\nChoose an operator from the following list:");
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.Write("\nYour choice: ");
                        string? op = Console.ReadLine();

                        if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                        {
                            Console.WriteLine("Invalid choice. Please enter a valid operand");
                        }


                        else
                        {
                            try
                            {
                                result = calc.DoOperation(cleanNum1, cleanNum2, op);
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a math error\n");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("- Whats next? -");
                                    Console.WriteLine("------------------------\n");
                                    Console.WriteLine("Your result: {0:0.##}", result);

                                    switch (op)
                                    {
                                        case "a":
                                            op = "+";
                                            break;
                                        case "s":
                                            op = "-";
                                            break;
                                        case "m":
                                            op = "x";
                                            break;
                                        case "d":
                                            op = "/";
                                            break;
                                    }

                                    history.Add($"{cleanNum1} {op} {cleanNum2} = {result}", result);
                                    count++;
                                    Console.WriteLine($"\nTotal calculations completed: {count}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
                            }
                        }
                        Console.WriteLine("------------------------\n");

                        bool done3 = false;
                        while (!done3)
                        {
                            Console.WriteLine("1 - Another calculation");
                            Console.WriteLine("2 - Return to menu");
                            Console.WriteLine("3 - Exit program");
                            Console.Write("\nHow would you like to proceed? Choose an option: ");
                            string input = Console.ReadLine();

                            switch (input)
                            {
                                case "1":
                                    done2 = true;
                                    Calculation();
                                    break;
                                case "2":
                                    done2 = true;
                                    Menu();
                                    break;
                                case "3":
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please enter a valid menu option (1-3");
                                    break;
                            }
                        }
                    }
                    calc.Finish();
                    return;
                }
            }
        }
    }
}
