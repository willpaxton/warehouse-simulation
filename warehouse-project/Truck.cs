using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    public class Truck
    {
        string driver;
        string deliveryCompany;
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
