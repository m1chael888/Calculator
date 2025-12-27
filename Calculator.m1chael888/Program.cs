using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram.m1chael888
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();
            var history = new Dictionary<List<double>, string>();
            int count = 0;

            Menu();

            //////////
            void Menu()
            {
                Console.Clear();
                Console.WriteLine("- Calculator Menu -");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("1 - Proceed to calucation");
                Console.WriteLine("2 - View calculation history");
                Console.WriteLine("3 - Exit program");
                Console.Write("\nEnter the number of your desired menu option: ");
                string input = Console.ReadLine();

                bool done = false;
                while (!done)
                {
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
                            input = Console.ReadLine();
                            break;
                    }
                }
            }

            /////////////////
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
                        Console.WriteLine($"Calculation {calcNum}: {calculation.Value}");
                    }
                }

                Console.WriteLine("------------------------");
                Console.WriteLine("- What's Next? -");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("1 - New calculation with previous results");
                Console.WriteLine("2 - Delete History (This cannot be undone)");
                Console.WriteLine("3 - Return to menu");
                Console.WriteLine("4 - Exit program");
                Console.Write("\nHow would you like to proceed? Choose an option: ");
                string input = Console.ReadLine();

                bool done = false;
                while (!done)
                {
                    switch (input)
                    {
                        case "1":
                            ResultCalculation();
                            break;
                        case "2":
                            done = true;
                            history.Clear();
                            ViewHistory();
                            break;
                        case "3":
                            done = true;
                            Menu();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Write("Invalid choice. Please enter a valid menu option (1-4): ");
                            input = Console.ReadLine();
                            break;
                    }
                }
            }

            ///////////////////////
            void ResultCalculation()
            {
                string input1 = ""; double num1;
                string input2 = ""; double num2;

                if (history.Count() > 1)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("- Conduct calculation with existing results -");
                    Console.WriteLine("------------------------\n");

                    Console.Write("Enter the Question number representing the first result youd like to use: ");
                    input1 = Console.ReadLine();
                    while (!(Convert.ToInt32(input1) >= 0 && Convert.ToInt32(input1) <= history.Count))
                    {
                        Console.Write($"This is not valid input. Please enter a numeric value (1-{history.Count}): ");
                        input1 = Console.ReadLine();
                    }
                    num1 = GetResult(Convert.ToDouble(input1));
                    Console.WriteLine($"First number: {num1}");

                    Console.Write("Enter the Question number representing the second result youd like to use: ");
                    input2 = Console.ReadLine();
                    while (!(Convert.ToInt32(input2) >= 0 && Convert.ToInt32(input2) <= history.Count))
                    {
                        Console.Write($"This is not valid input. Please enter a numeric value (1-{history.Count}): ");
                        input2 = Console.ReadLine();
                    }
                    num2 = GetResult(Convert.ToDouble(input2));
                    Console.WriteLine($"Second number: {num2}");

                    Calculation(num1.ToString(), num2.ToString());
                }
                else
                {
                    Console.Write("There are not enough existing results to calculate with. Choose a different option: ");
                }
            }

            /////////////////
            void Calculation(string input1 = "", string input2 = "")
            {
                bool done = false;
                while (!done)
                {
                    double result = 0;

                    if (input1 == "" && input2 == "")
                    {
                        Console.WriteLine("------------------------");
                        Console.WriteLine("- Conduct Calculation -");
                        Console.WriteLine("------------------------\n");
                        Console.Write("Type a number, and then press Enter: ");
                        input1 = Console.ReadLine();

                        double num1 = 0;
                        while (!double.TryParse(input1, out num1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            input1 = Console.ReadLine();
                        }

                        Console.Write("Type another number, and then press Enter: ");
                        input2 = Console.ReadLine();

                        double num2 = 0;
                        while (!double.TryParse(input2, out num2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            input2 = Console.ReadLine();
                        }
                    }
                    
                    double cleanNum1 = Convert.ToDouble(input1);
                    double cleanNum2 = Convert.ToDouble(input2);

                    bool done2 = false;
                    while (!done2)
                    {
                        Console.WriteLine("\nChoose an operator from the following list:");
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.Write("\nYour choice: ");
                        string op = Console.ReadLine();

                        while (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                        {
                            Console.Write("Invalid choice. Please enter a valid operand: ");
                            op = Console.ReadLine();
                        }

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
                                Console.WriteLine("- Calcuation complete -");
                                Console.WriteLine("------------------------\n");
                                Console.WriteLine("Your result: {0:0.##}", result);

                                count++;
                                history.Add([count, result], $"{cleanNum1} {GetOp(op)} {cleanNum2} = {result}");
                                Console.WriteLine($"\nTotal calculations completed: {count}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
                        }
                        
                        Console.WriteLine("------------------------");
                        Console.WriteLine("- What's Next? -");
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
                                    Console.Clear();
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
                                    Console.Write("Invalid choice. Please enter a valid menu option (1-3): ");
                                    input = Console.ReadLine();
                                    break;
                            }
                        }
                    }
                    calc.Finish();
                    return;
                }
            }

            /////////////
            string GetOp(string op)
            {
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

                return op;
            }

            /////////////////
            double GetResult(double id)
            {
                foreach (var record in history)
                {
                    if (record.Key[0] == id) return record.Key[1];
                }
                return 0;
            }
        }
    }
}