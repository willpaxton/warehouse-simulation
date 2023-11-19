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
    /// 
    /// </summary>
    public class Dock
    {
        public string Id { get; set; } // probably should be incremental

        public Queue<Truck> Line = new Queue<Truck>();

        public double TotalSales { get; set; } = 0;

        public int TotalCrates { get; set; } = 0;

        public int TotalTrucks { get; set; } = 0;

        public int TimeInUse { get; set; } = 0;

        public int TimeNotInUse { get; set; } = 0;

        public Truck? ActiveTruck => Line.Count() != 0 ? Line.Peek() : null;

        public void JoinLine(Truck truck)
        {
            Line.Enqueue(truck);    
        }

        public Truck SendOff()
        {
            TotalTrucks++;
            return Line.Dequeue();
        }

        public Dock(int position)
        {
            Id = position.ToString();
        }

    }
}
