namespace EhotelBuffet.Ui;

public class Program
{
    public Random Random = new Random();
    public static void Main(string[] args)
    {
        Ui ui = new Ui();
        ui.Run();
        do
        {
            Console.WriteLine("Do you want to continue? (yes/no): ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "yes" || userInput == "y")
            {
                Console.Clear();
                ui.Run();
            }
            else if (userInput == "no" || userInput == "n")
            {
                Console.WriteLine("Exiting the program. Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes/y' or 'no/n'.");
            }
        } while (true);
        }
    }
    
