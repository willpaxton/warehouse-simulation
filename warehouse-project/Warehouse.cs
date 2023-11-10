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
                Dock dock = new Dock(i + 1);
                Docks.Add(dock);
            }

            // So for now, this is only working through one time period, everything below will need to be looped

            // Adding new trucks to the warehouse queue
            Random randy = new Random();
            for (int i = 0; i < randy.Next(1, 5); i++)
            {
                Truck newTruck = new Truck();
                for (int j = 0; j < randy.Next(1, 5); j++) newTruck.Load(new Crate());
                this.Entrance.Enqueue(newTruck);
            }

            foreach (Dock dock in Docks)
            {
                Unload(dock);
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

        /// <summary>
        /// Completes one time span
        /// </summary>
        public void Unload(Dock dock)
        {
            while (this.Entrance.Count > 0)
            {
                Dock shortestDock = null;
                int shortestLineLength = int.MaxValue;

                foreach (Dock dockLen in Docks)
                {
                    if (dockLen.Line.Count() < shortestLineLength)
                    {
                        shortestDock = dockLen; 
                        shortestLineLength = dockLen.Line.Count();
                    }
                    
                }

                shortestDock.JoinLine(this.Entrance.Dequeue());

            }

            // Check if dock line empty, if so, add truck
            // if (dock.ActiveTruck == null) dock.JoinLine(this.Entrance.Dequeue());

            // Unload
            // need to implement totalcrates count
            if (dock.ActiveTruck != null)
            {
                Crate unloadedCrate = dock.ActiveTruck.Unload();
                double value = unloadedCrate.GetPrice();
                dock.TotalSales += value;
                dock.TimeInUse++;
            }
            else dock.TimeNotInUse++;

            // Swap Trucks
            if (dock.ActiveTruck is not null)
            {
                if (dock.ActiveTruck.Trailer.Count == 0) dock.SendOff();
            }


            // Log
        }


    }
}
