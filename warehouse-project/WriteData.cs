using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    internal class WriteData
    {
        /// <summary>
        /// Edits a csv file to create a log of all the data ran from this program
        /// </summary>
        /// <param name="numOfDocksOpen">name of the file ran by the sorting algorithm</param>
        /// <param name="longestLineAtAnyLoadingDock">data type of the file ran either Book or Integer</param>
        /// <param name="totalNumOfTrucksProcessed">type of algorithm ran either recursive or iterative</param>
        /// <param name="totalNumOfCratesUnloaded">amount of time it took to finish sorting list</param>
        /// <param name="totalValOfCratesUnloaded">name of the file ran by the sorting algorithm</param>
        /// <param name="avgValOfEachCrate">data type of the file ran either Book or Integer</param>
        /// <param name="avgValOfEachTruck">type of algorithm ran either recursive or iterative</param>
        /// <param name="totalTimeEachDockWasUsed">amount of time it took to finish sorting list</param>
        /// <param name="totalAmountOfTimeDockWasntUsed">name of the file ran by the sorting algorithm</param>
        /// <param name="avgTimeDockWasInUse">data type of the file ran either Book or Integer</param>
        /// <param name="totalCostOfOperatingEachDock">type of algorithm ran either recursive or iterative</param>
        /// <param name="totalRevenueOfTheWarehouse">amount of time it took to finish sorting list</param>
        /// <exception cref="FileLoadException">file could not be read or isn't in the correct format to be written to</exception>
        public void WriteDataFile(int numOfDocksOpen, int longestLineAtAnyLoadingDock, int totalNumOfTrucksProcessed, 
            int totalNumOfCratesUnloaded, int totalValOfCratesUnloaded, int avgValOfEachCrate, int avgValOfEachTruck,
             int totalTimeEachDockWasUsed, int totalAmountOfTimeDockWasntUsed, int totalAmountOfTimeDockWasInUse, 
             int avgTimeDockWasInUse, int totalCostOfOperatingEachDock, int totalRevenueOfTheWarehouse)
        {
            try
            {
                StreamWriter rwr = new StreamWriter($@"..\..\..\data\output\spreadsheetData.csv", true);

                rwr.WriteLine(
                    $"There were {numOfDocksOpen} docks open. \n" +
                    $"The Longest Line at any loading dock was {longestLineAtAnyLoadingDock}. \n" +
                    $"There were a total of {totalNumOfTrucksProcessed} trucks processed. \n" +
                    $"There were a total of {totalNumOfCratesUnloaded} crates unloaded. \n" +
                    $"The total value of crates unloaded was ${totalValOfCratesUnloaded}. \n" +
                    $"The average value of each crate was {avgValOfEachCrate}. \n" + //Find Good Way to do it
                    $"The average value of each truck was {avgValOfEachTruck}. \n" + //Find Good Way to do it
                    $"The total time each dock was used was {totalTimeEachDockWasUsed}. \n" + //Find Good Way to do it
                    $"The total amount of time each dock wasn't used is {totalAmountOfTimeDockWasntUsed}. \n" + //Find Good Way to do it
                    $"The total amount of time each dock was in use is {totalAmountOfTimeDockWasInUse}. \n" + //Find Good Way to do it
                    $"The average time dock was in use is {avgTimeDockWasInUse}. \n" + //Find Good Way to do it
                    $"The total cost of operating each is {totalCostOfOperatingEachDock}. \n" + //Find Good Way to do it
                    $"The total revenue of the warehouse is {totalRevenueOfTheWarehouse}. ");

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
