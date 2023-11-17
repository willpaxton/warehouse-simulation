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

            WriteData wr = new WriteData();

            simulation.Run(NUMBER_OF_DOCKS, NUMBER_OF_MAX_TRUCKS, NUMBER_OF_MAX_CRATES);
;
        }
    }
}