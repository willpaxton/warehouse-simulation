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
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    /// <summary>
    /// An individual crate object with a monetary value
    /// </summary>
    public class Crate
    {
        private string id;
        private double price;

        public Crate()
        {
            this.price = GetPrice();
            id = GetID();
        }

        public string GetID()
        {
            Random rnd = new Random();
            int intID = rnd.Next(0, 1000000);
            return intID.ToString();
        }

        public double GetPrice()
        {
            Random rnd = new Random();
            return rnd.Next(50, 501);
        }

        public override string ToString()
        {
            return this.price.ToString();
        }
    }
}
