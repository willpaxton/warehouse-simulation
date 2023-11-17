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
        /// Edits a csv file to create a log of all the crates from this program
        /// </summary>
        /// <param name="timeIncrement">The Time Increment it was unloaded</param>
        /// <param name="truckDriverName">Truck Drivers Name</param>
        /// <param name="deliveryCompanyName">Name of the Delivery Company</param>
        /// <param name="crateID">This crates personal ID</param>
        /// <param name="crateValue">The value of this crate</param>
        /// <param name="scenarioNum">The Number of the Scenario that is loaded</param>
        /// <exception cref="FileLoadException">file could not be read or isn't in the correct format to be written to</exception>
        public void WriteDataFile(int timeIncrement, string truckDriverName, string deliveryCompanyName, int crateID, int crateValue, int scenarioNum)
        {
            try
            {
                StreamWriter rwr = new StreamWriter($@"..\..\..\data\output\crateData.csv", true);

                rwr.WriteLine($"" +
                    $"Time Increment Unloaded: {timeIncrement}" +
                    $"Truck Driver's Name: {truckDriverName}" +
                    $"Delivery Company's Name: {deliveryCompanyName}" +
                    $"Crate's ID: {crateID}" +
                    $"Crate's Value: {crateValue}");

                if (scenarioNum == 1 ) 
                {
                    Console.WriteLine("This crate has been unloaded, but there are more crates to unload from this Truck");
                }
                else if (scenarioNum == 2)
                {
                    Console.WriteLine("This crate has been unloaded, and this Truck has no more crates to unload, and another truck is in the dock already");
                }
                else if (scenarioNum == 3)
                {
                    Console.WriteLine("This Crate has been unloaded, and this Truck has no more crates to unload, but another truck is not in the dock");
                }
                else
                {
                    Console.WriteLine("Something has gone wrong.");
                }

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
