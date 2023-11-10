using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    public class Warehouse
    {
        List<Dock> Docks = new List<Dock>();

        Queue<Truck> Entrance = new Queue<Truck>();

        // accepts some arguments (constants) from the driver
        public void Run(int numberOfDocks)
        {
            // Creates the docks being used in this simulation
            for (int i = 0; i < numberOfDocks; i++)
            {
                Docks[i] = new Dock(); 
            }

            foreach (Dock dock in Docks)
            {
                // Unload

                // Swap Trucks

                // Count Stats

                // Log
            }
        }

        /// <summary>
        /// Takes data and formats it as a string to save to a csv file
        /// </summary>
        // We just gotta figure out how to do this, but it's our method would be better
        // Maybe returns a boolean if successful?
        public void Log()
        {

        }


    }
}
