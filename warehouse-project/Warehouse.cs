///////////////////////////////////////////////////////////////////////////////
//
// Author: Will Paxton & Nick Trahan
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 3
// Description: Programming a Warehouse to demostrate profiency with data structures.
// 
//
/////////////////////////////////////////////////////////////////////////////// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    /// <summary>
    /// The warehouse object controls all aspects of the warehouse and runs a simulation
    /// </summary>
    public class Warehouse
    {
        List<Dock> Docks = new List<Dock>();

        Queue<Truck> Entrance = new Queue<Truck>();

        int numberOfIncrements = 0;


        int totalNumOfDocks; // done
        int longestLineAtAnyLoadingDock = 0; // done
        int totalNumOfTrucksProcessed; // done, counts fully unloaded trucks
        int totalNumOfCratesUnloaded; // done
        double totalValOfCratesUnloaded; // done
        double avgValOfEachCrate; // done i think
        int trucksFullyUnloaded = 0;
        double averageTruckValue = 0;

        int fileNum = -1;
        string? dataFile;



        /// <summary>
        /// Runs a simulation of the warehouse
        /// </summary>
        /// <param name="numberOfDocks">The number of docks the warehouse needs to have</param>
        /// <param name="numberOfMaxCrates">The maximum number of crates each truck can hold</param>
        /// <param name="numberOfDays">Declares how many time increments the simulation will run for</param>
        public void Run(int numberOfDocks, int numberOfMaxCrates, int numberOfDays)
        {
            // setting up some basic stats
            totalNumOfDocks = numberOfDocks;
            numberOfIncrements = numberOfDays * 48;

            // finds a "session number" for the crateData csv
            do
            {
                fileNum++;
                dataFile = $"data\\crateData{fileNum}.csv";
                
            } while (File.Exists(dataFile));

            // Creates the docks being used in this simulation
            for (int i = 0; i < numberOfDocks; i++)
            {
                Dock dock = new Dock(i + 1);
                Docks.Add(dock);
            }

            // Adding new trucks to the warehouse queue
            Random randy = new Random();
            

            //Console.WriteLine("!");

            for (int x = 0; x < numberOfIncrements; x++)
            {
                int timeIncrement = x % 48;

                if (DoesTruckArrive(timeIncrement))
                {
                    Console.WriteLine("truck A");
                    CreateTruck(numberOfMaxCrates);
                    

                    if (Math.Abs(timeIncrement - 24) <= 6 && randy.Next(0, 100) >= 100 - 33) // 33% chance of second truck arriving during high period
                    {
                        Console.WriteLine("truck B");
                        CreateTruck(numberOfMaxCrates);
                    }
                }

                foreach (Dock dock in Docks)
                {
                    Unload(dock, x);
                    
                }
                Console.WriteLine(ConvertToDateTime(x));
            }
            
        }

        /// <summary>
        /// Creates a new truck object and loads it with crates
        /// </summary>
        /// <param name="numberOfMaxCrates">The maximum number of crates a truck can hold</param>
        public void CreateTruck(int numberOfMaxCrates)
        {
            Random randy = new Random();
            Truck newTruck = new Truck();
            int numOfCrates = randy.Next(1, numberOfMaxCrates);
            for (int j = 0; j < numOfCrates; j++) newTruck.Load(new Crate());
            this.Entrance.Enqueue(newTruck);
            totalNumOfTrucksProcessed++;
        }

        /// <summary>
        /// Runs a scenario to add a truck to the warehouse queue if certain conditions are met
        /// </summary>
        /// <param name="TimeIncrement">The current time of day during the simulation</param>
        /// <returns>A boolean value based on if a truck was added to the queue or not</returns>
        public bool DoesTruckArrive(int TimeIncrement)
        {
            Random randy = new Random();

            if (Math.Abs(TimeIncrement - 24) >= 16)
            {
                
                if (randy.Next(0,100) > 100 - 33) // 33% chance
                {

                    return true;
                }
                else return false;
            }

            else if (Math.Abs(TimeIncrement - 24) >= 6)
            {
                
                if (randy.Next(0, 100) > 100 - 50) // 50%
                {
                    return true;
                }
                else return false;
            }

            else
            {
                
                if (randy.Next(0, 100) > 100 - 80) // 80% chance
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Completes one time span of the simulation
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

            // Unload
            if (dock.ActiveTruck != null)
            {
                Crate unloadedCrate = dock.ActiveTruck.Unload();
                double value = unloadedCrate.GetPrice();
                dock.TotalSales += value;
             
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
            }
            else
            {
                dock.TimeNotInUse++;
                
            }
            // Swap Trucks
            if (dock.ActiveTruck is not null)
            {
                if (dock.ActiveTruck.Trailer.Count == 0)
                {
                    averageTruckValue = ((averageTruckValue * trucksFullyUnloaded) + dock.ActiveTruck.totalValue) / (trucksFullyUnloaded + 1);
                    dock.SendOff();
                    trucksFullyUnloaded++;
                }
            }
        }

        /// <summary>
        /// Takes the current daily time increment and converts into into a time
        /// </summary>
        /// <param name="currentTimeIncrement">The current time increment</param>
        /// <returns>A DateTime object with the correct time of day (date starts at 1/1/1970 since time is what is being tracked here)</returns>
        internal DateTime ConvertToDateTime(int currentTimeIncrement)
        {
            DateTime currentTime = new DateTime(1970, 1, 1, 2, 0, 0); // Should resemble what time increment 0 is

            for (int x = 0; x < currentTimeIncrement; x++)
            {
                currentTime = currentTime.AddMinutes(30);
            }

            return currentTime;
        }

        /// <summary>
        /// Prints a report to the console with data about the simulation
        /// </summary>
        public void CreateReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"There were {totalNumOfDocks} docks open during this simulation\n");
            sb.Append($"The longest line to build up at a dock reached {longestLineAtAnyLoadingDock} trucks long (including the one unloading)\n");
            sb.Append($"A total of {totalNumOfTrucksProcessed} trucks were processed at the enterance of the warehouse\n");
            sb.Append($"There were a total of {totalNumOfCratesUnloaded} crates unloaded during the simulation\n");
            sb.Append($"The total value of every crate unloaded during the simulation reached {totalValOfCratesUnloaded.ToString("C")}\n");
            sb.Append($"The average value of each crate unloaded was {Math.Round(avgValOfEachCrate, 2).ToString("C")}\n");
            sb.Append($"The average value of each truck was {averageTruckValue.ToString("C")}\n");
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


                // finds a new filename to use
                
                string fileNameWithAddon = dataFile;

                if (!File.Exists(fileNameWithAddon)) {
                    using (StreamWriter createWriter = File.AppendText(fileNameWithAddon))
                    {
                        createWriter.WriteLine(header);
                    };

                }
                
                StreamWriter rwr = new StreamWriter(fileNameWithAddon, true);


                rwr.WriteLine($"" +
                    $"{ConvertToDateTime(currentTimePeriod).Day}," +
                    $"{ConvertToDateTime(currentTimePeriod).TimeOfDay}," +
                    $"{companyName}," +
                    $"{lastName}," +
                    $"{firstName}," +
                    $"{crateID}," +
                    $"{crateValue.ToString("C")}," +
                    $"{scenario}");

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
