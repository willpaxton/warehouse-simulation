﻿namespace warehouse_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int NUMBER_OF_DOCKS = 1;

            Warehouse simulation = new Warehouse();

            simulation.Run(NUMBER_OF_DOCKS);
        }
    }
}