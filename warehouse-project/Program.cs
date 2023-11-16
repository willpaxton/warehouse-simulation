namespace warehouse_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int NUMBER_OF_DOCKS = 5;
            int NUMBER_OF_MAX_TRUCKS = 100;
            int NUMBER_OF_MAX_CRATES = 20;

            Warehouse simulation = new Warehouse();

            simulation.Run(NUMBER_OF_DOCKS, NUMBER_OF_MAX_TRUCKS, NUMBER_OF_MAX_CRATES);

            simulation.CreateReport();
        }
    }
}