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
                Dock dock = new Dock();
                Docks.Add(dock);
            }

            Docks[0].JoinLine(new Truck());

            Docks[0].ActiveTruck.Load(new Crate());

            foreach (Dock dock in Docks)
            {
                // Unload
                // need to implement totalcrates count
                if (dock.ActiveTruck != null)
                {
                    Crate unloadedCrate = dock.ActiveTruck.Unload();
                    double value = unloadedCrate.GetPrice();
                    dock.TotalSales += value;
                    dock.TimeInUse++;
                } else
                {
                    dock.TimeNotInUse++;
                }

                // Swap Trucks
                if (dock.ActiveTruck.Trailer.Count == 0) dock.SendOff();
                

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
