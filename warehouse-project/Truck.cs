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
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    /// <summary>
    /// A truck object that is filled with crates
    /// </summary>
    public class Truck
    {
        public string driverFirstName;
        public string driverLastName;
        public string deliveryCompany; 
        public Stack<Crate> Trailer = new Stack<Crate>();
        public double totalValue = 0;

        public void Load(Crate crate)
        {
            Trailer.Push(crate);
        }

        public Crate Unload() 
        { 
            Crate unloadedCrate = Trailer.Pop();
            totalValue += unloadedCrate.GetPrice();
            return unloadedCrate;
        }

        public Truck()
        {
            Random random = new Random();
            
            string line = File.ReadLines("data\\driverData.csv").Skip(random.Next(0,1000)).Take(1).First();

            string[] data = line.Split(",");
            driverFirstName = data[0];
            driverLastName = data[1];
            deliveryCompany = data[2];
            

            //Console.WriteLine(line);
            
        }
    }
}
