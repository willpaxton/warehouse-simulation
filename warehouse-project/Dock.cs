﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    public class Dock
    {
        public String Id { get; set; } // probably should be incremental

        Queue<Truck> Line = new Queue<Truck>();

        public double TotalSales { get; set; } = 0;

        public int TotalCrates { get; set; } = 0;

        public int TotalTrucks { get; set; } = 0;

        public int TimeInUse { get; set; } = 0;

        public int TimeNotInUse { get; set; } = 0;

        public void JoinLine(Truck truck)
        {

        }

        public Truck SendOff()
        {
            
        }
    }
}
