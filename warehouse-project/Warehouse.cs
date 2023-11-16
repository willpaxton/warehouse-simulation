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

        int numThatTruckHasToBeLargerThenFromOneHundred = 65; // i.e. 60 would be a 40% chance (Mess with at end to balance)

        int totalNumOfDocks; // done
        int longestLineAtAnyLoadingDock = 0; // done
        int totalNumOfTrucksProcessed; // done, counts fully unloaded trucks
        int totalNumOfCratesUnloaded; // done
        double totalValOfCratesUnloaded; // done
        double avgValOfEachCrate; // done i think
        double avgValOfEachTruck; // ???
        int totalTimeEachDockWasUsed; 
        int totalAmountOfTimeDockWasntUsed; // code implemented, maybe should change to datetime object, also i think its in the wrong spot
        int totalAmountOfTimeDockWasInUse;
        int avgTimeDockWasInUse;
        int totalCostOfOperatingEachDock;
        int totalRevenueOfTheWarehouse;

        // accepts some arguments (constants) from the driver
        public void Run(int numberOfDocks, int numberOfStartingTrucks, int numberOfMaxCrates)
        {
            // setting up some basic stats
            totalNumOfDocks = numberOfDocks;

            // Creates the docks being used in this simulation
            for (int i = 0; i < numberOfDocks; i++)
            {
                Dock dock = new Dock(i + 1);
                Docks.Add(dock);
            }

            // So for now, this is only working through one time period, everything below will need to be looped

            // Adding new trucks to the warehouse queue
            Random randy = new Random();
            int numOfTrucks = randy.Next(1, numberOfStartingTrucks);

            Console.WriteLine("!");

            for (int x = 0; x < 48; x++)
            {
                if (DoesTruckArrive(x))
                {
                    CreateTruck(numberOfMaxCrates);

                    if (randy.Next(0,10) > 8)
                    {
                        CreateTruck(numberOfMaxCrates);
                    }
                }

                foreach (Dock dock in Docks)
                {
                    Unload(dock);
                }
                Console.WriteLine(x);
            }
            
        }

        public void CreateTruck(int numberOfMaxCrates)
        {
            Random randy = new Random();
            Truck newTruck = new Truck();
            int numOfCrates = randy.Next(1, numberOfMaxCrates);
            for (int j = 0; j < numOfCrates; j++) newTruck.Load(new Crate());
            this.Entrance.Enqueue(newTruck);
            Console.WriteLine("omg a new truck has been birthed");
        }

        /// <summary>
        /// Takes data and formats it as a string to save to a csv file
        /// </summary>
        // We just gotta figure out how to do this, but it's our method would be better
        // Maybe returns a boolean if successful?
        internal void Log()
        {
            // im too lazy to write to a file rn
            for (int i = 0; i < Docks.Count; i++)
            {
                Console.WriteLine($"Dock {i+1} - {Docks[i].Line.Count}");
            }
            Console.WriteLine("Unloaded");

            //WriteData.WriteDataFile(/*num of docks open*/, /*longest line at any dock*/, /*total num of trucks processed*/, /*total num of trucks processed*/, /*total num of crates processed...*/);
        }

        public bool DoesTruckArrive(int TimeIncrement)
        {
            Random randy = new Random();
            Random randall = new Random();
            Random randina = new Random();
            Random rina = new Random();

            if (Math.Abs(TimeIncrement - 24) >= 16)
            {
                //Roll 1 Times
                if (randy.Next(0,100) > numThatTruckHasToBeLargerThenFromOneHundred)
                {
                    return true;
                }
                else return false;
            }

            else if (Math.Abs(TimeIncrement - 24) >= 6)
            {
                //Roll 2 Times
                if (randy.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred || randall.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred)
                {
                    return true;
                }
                else return false;
            }

            else // if (Math.Abs(TimeIncrement - 24) < 30)
            {
                // Roll 4 Times
                if (randy.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred || randall.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred || randina.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred || rina.Next(0, 100) > numThatTruckHasToBeLargerThenFromOneHundred)
                {
                    return true;
                }
                else return false;
            }
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
                    if (dockLen.Line.Count() > this.longestLineAtAnyLoadingDock) this.longestLineAtAnyLoadingDock = dockLen.Line.Count();

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
                this.totalNumOfCratesUnloaded++;
                double value = unloadedCrate.GetPrice();
                dock.TotalSales += value;

                // Update Stats
                this.totalNumOfCratesUnloaded++;

                this.totalValOfCratesUnloaded += value;

                // Calculate updated average
                this.avgValOfEachCrate += (this.avgValOfEachCrate * this.totalNumOfCratesUnloaded + value);

                dock.TimeInUse++;
                //Log();
            }
            else
            {
                dock.TimeNotInUse++;
                this.totalAmountOfTimeDockWasntUsed++; // adds one, maybe should add 30 minutes?
            }
            // Swap Trucks
            if (dock.ActiveTruck is not null)
            {
                if (dock.ActiveTruck.Trailer.Count == 0) dock.SendOff();
                this.totalNumOfTrucksProcessed++;

            }


            // Log
        }

        /// <summary>
        /// Takes the current daily time increment and converts into into a time
        /// </summary>
        /// <param name="currentTimeIncrement">The current time increment</param>
        /// <returns>A DateTime object with the correct time of day (date will not be correct)</returns>
        internal DateTime ConvertToDateTime(int currentTimeIncrement)
        {
            DateTime currentTime = new DateTime(1970, 1, 1, 0, 0, 0); // Should resemble what time increment 0 is

            for (int x = 0; x < currentTimeIncrement; x++)
            {
                currentTime.AddMinutes(30);
            }

            return currentTime;
        }



    }
}
