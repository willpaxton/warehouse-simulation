using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    internal class Truck
    {
        string driver; //Assign Random Driver
        string deliveryCompany; //Assign Delivery Company From The Driver Line
        Stack<Crate> Trailer;

        void Load(Crate crate)
        {
            Trailer.Push(crate);
        }

        Crate Unload() 
        { 
            return Trailer.Pop();
        }
    }
}
