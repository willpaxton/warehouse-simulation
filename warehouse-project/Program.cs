using System.Text.RegularExpressions;

namespace warehouse_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Regex rx = new Regex(@"[0-9]+");

            string input;

            do {
                Console.Write("How many docks would you like to open? (numbers only): ");
                input = Console.ReadLine();
            } while (!rx.IsMatch(input));

            int numOfDocks = Int32.Parse(input);

            do
            {
                Console.Write("\nWhat is the maximum number of crates per truck? (numbers only): ");
                input = Console.ReadLine();
            } while (!rx.IsMatch(input));

            int numOfCrates = Int32.Parse(input);

            do
            {
                Console.Write("\nHow many days do you want to run the simulation for? (numbers only, 1 day = 48 increments): ");
                input = Console.ReadLine();
            } while (!rx.IsMatch(input));

            int numOfDays = Int32.Parse(input);

            Warehouse simulation = new Warehouse();

            simulation.Run(numOfDocks, numOfCrates, numOfDays);

            Console.Write("\n\n\n\n\n");

            simulation.CreateReport();
;
        }
    }
}