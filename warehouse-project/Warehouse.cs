using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    public class Warehouse
    {
        List<Dock> Docks = new List<Dock>();

        Queue<Truck> Entrance = new Queue<Truck>();

        int numberOfIncrements = 0;

        int numThatTruckHasToBeLargerThenFromOneHundred = 65; // i.e. 60 would be a 40% chance (Mess with at end to balance)

        int totalNumOfDocks; // done
        int longestLineAtAnyLoadingDock = 0; // done
        int totalNumOfTrucksProcessed; // done, counts fully unloaded trucks
        int totalNumOfCratesUnloaded; // done
        double totalValOfCratesUnloaded; // done
        double avgValOfEachCrate; // done i think
        double avgValOfEachTruck; // ???
        int totalTimeEachDockWasUsed; // ???
        int totalAmountOfTimeDockWasntUsed; // redundant
        int totalAmountOfTimeDockWasInUse; //  redundant
        int avgTimeDockWasInUse; // redundant
        int totalCostOfOperatingEachDock; // calculate from total increments passed * incremental cost
        int totalRevenueOfTheWarehouse;

        // accepts some arguments (constants) from the driver
        public void Run(int numberOfDocks, int numberOfMaxCrates, int numberOfDays)
        {
            // setting up some basic stats
            totalNumOfDocks = numberOfDocks;
            numberOfIncrements = numberOfDays * 48;

            // Creates the docks being used in this simulation
            for (int i = 0; i < numberOfDocks; i++)
            {
                Dock dock = new Dock(i + 1);
                Docks.Add(dock);
            }

            // So for now, this is only working through one time period, everything below will need to be looped

            // Adding new trucks to the warehouse queue
            Random randy = new Random();
            

            //Console.WriteLine("!");

            for (int x = 0; x < numberOfIncrements; x++)
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
                    Unload(dock, x);
                    
                }
                // Console.WriteLine(x);
            }
            
        }

        public void CreateTruck(int numberOfMaxCrates)
        {
            Random randy = new Random();
            Truck newTruck = new Truck();
            int numOfCrates = randy.Next(1, numberOfMaxCrates);
            for (int j = 0; j < numOfCrates; j++) newTruck.Load(new Crate());
            this.Entrance.Enqueue(newTruck);
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
        public void Unload(Dock dock, int currentTimePeriod)
        {
            if (this.Entrance.Count > 0)
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
                double value = unloadedCrate.GetPrice();
                dock.TotalSales += value;

                // Update Stats
                

                this.totalValOfCratesUnloaded += value;

                // Calculate updated average
                this.avgValOfEachCrate = (this.avgValOfEachCrate * this.totalNumOfCratesUnloaded + value) / (totalNumOfCratesUnloaded + 1);

                this.totalNumOfCratesUnloaded++;

                dock.TimeInUse++;

                // R - Crates Remain
                // S - Trucks Swapped
                // E - Queue Empty
                char currentScenario = 'R';

                if (dock.ActiveTruck.Trailer.Count == 0)
                { 
                    if (dock.Line.Count == 1)
                    {
                        currentScenario = 'E';
                    } else
                    {
                        currentScenario = 'S';
                    }
                }

                    WriteDataFile(currentTimePeriod, unloadedCrate.GetID(), unloadedCrate.GetPrice(), dock.ActiveTruck.deliveryCompany, dock.ActiveTruck.driverLastName, dock.ActiveTruck.driverFirstName, currentScenario);

                //Log();
            }
            else
            {
                dock.TimeNotInUse++;
                
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
            DateTime currentTime = new DateTime(1970, 1, 1, 2, 0, 0); // Should resemble what time increment 0 is

            for (int x = 0; x < currentTimeIncrement; x++)
            {
                currentTime = currentTime.AddMinutes(30);
            }

            return currentTime;
        }

        public void CreateReport()
        {
            //x The number of docks open during the simulation.
            //x The longest line at any loading dock during the simulation.
            //x The total number of trucks that were processed during the simulation.
            //x The total number of crates that were unloaded during the simulation.
            //x The total value of the crates that were unloaded during the simulation.
            //x The average value of each crate unloaded during the simulation.
            //x The average value of each truck unloaded during the simulation.
            //x The total amount of time that a dock was in use.
            //x The total amount of time that a dock was not in use.
            //x The average amount of time that a dock was in use.
            // The total cost of operating each dock.
            // The total revenue of the warehouse(total value of crates – total operating cost)

            StringBuilder sb = new StringBuilder();

            sb.Append($"There were {totalNumOfDocks} docks open during this simulation\n");
            sb.Append($"The longest line to build up at a dock reached {longestLineAtAnyLoadingDock} trucks long (including the one unloading)\n");
            sb.Append($"A total of {totalNumOfTrucksProcessed} were unloaded at the warehouse\n");
            sb.Append($"There were a total of {totalNumOfCratesUnloaded} crates unloaded during the simulation.\n");
            sb.Append($"The total value of every crate unloaded during the simulation reached {totalValOfCratesUnloaded.ToString("C")}\n");
            sb.Append($"The average value of each crate unloaded was {Math.Round(avgValOfEachCrate, 2).ToString("C")}\n");
            sb.Append($"The average value of each truck was {avgValOfEachTruck.ToString("C")}\n");
            sb.Append($"The follow shows the time statistics for each dock:\n");

            double totalRevenue = 0;

            foreach (Dock dock in Docks)
            {
                sb.Append($"\tDock {dock.Id} - Time in Use: {dock.TimeInUse} - Time Not in Use: {dock.TimeNotInUse} - Usage Percentage: {Math.Round((double)dock.TimeInUse / ((double)dock.TimeInUse + (double)dock.TimeNotInUse), 2) * 100}% - Profit of Dock: {dock.TotalSales.ToString("C")}\n");
                // Add profit of each dock

                totalRevenue += dock.TotalSales;
            }

            double totalCostToRun = this.Docks.Count * this.numberOfIncrements * 100;

            sb.Append($"It cost {totalCostToRun.ToString("C")} to run the warehouse\n"); // time increments * num of docks * 100
            sb.Append($"The total profit of the warehouse was {(totalRevenue - totalCostToRun).ToString("C")}\n");


            Console.WriteLine(sb);

        }

        /// <summary>
        /// Edits a csv file to create a log of all the crates from this program
        /// </summary>
        /// <param name="timeIncrement">The Time Increment it was unloaded</param>
        /// <param name="truckDriverName">Truck Drivers Name</param>
        /// <param name="deliveryCompanyName">Name of the Delivery Company</param>
        /// <param name="crateID">This crates personal ID</param>
        /// <param name="crateValue">The value of this crate</param>
        /// <param name="scenarioNum">The Number of the Scenario that is loaded</param>
        /// <exception cref="FileLoadException">file could not be read or isn't in the correct format to be written to</exception>
        public void WriteDataFile(int currentTimePeriod, string crateID, double crateValue, string companyName, string lastName, string firstName, char scenario)
        {
            try
            {

                string header = "day, time, del_comp, driver_lname, driver_fname, crate_id, crate_value, scenario";

                //string fileNameWithAddon = $"crateData{DateTime.Now.ToString().Replace(" ", "").Replace("/", "-").Replace(":", "")}.csv";

                string fileNameWithAddon = "crateDataaaaaaaaa.csv";

                if (!File.Exists(fileNameWithAddon)) {
                    using (StreamWriter createWriter = File.AppendText(fileNameWithAddon))
                    {
                        createWriter.WriteLine(header);
                    };

                }

                
                StreamWriter rwr = new StreamWriter(fileNameWithAddon, true);

                //rwr.Close();

                //StreamReader sr = new StreamReader(fileNameWithAddon);

               

                //if (sr.ReadLine() != header)
                //{
                //    rwr.WriteLine(header);
                //}

                rwr.WriteLine($"" +
                    $"{ConvertToDateTime(currentTimePeriod).Day}," +
                    $"{ConvertToDateTime(currentTimePeriod).TimeOfDay}," +
                    $"{companyName}," +
                    $"{lastName}," +
                    $"{firstName}," +
                    $"{crateID}," +
                    $"{crateValue.ToString("C")}," +
                    $"{scenario}");
                //if (scenarioNum == 1)
                //{
                //    Console.WriteLine("This crate has been unloaded, but there are more crates to unload from this Truck");
                //}
                //else if (scenarioNum == 2)
                //{
                //    Console.WriteLine("This crate has been unloaded, and this Truck has no more crates to unload, and another truck is in the dock already");
                //}
                //else if (scenarioNum == 3)
                //{
                //    Console.WriteLine("This Crate has been unloaded, and this Truck has no more crates to unload, but another truck is not in the dock");
                //}
                //else
                //{
                //    Console.WriteLine("Something has gone wrong.");
                //}

                rwr.Flush();

                rwr.Close();
            }
            catch
            {
                throw new FileLoadException();
            }
        }



    }
}
