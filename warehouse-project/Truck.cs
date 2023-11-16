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
        public Stack<Crate> Trailer = new Stack<Crate>();

        public void Load(Crate crate)
        {
            Trailer.Push(crate);
        }

        public Crate Unload() 
        { 
            return Trailer.Pop();
        }

        public Truck()
        {
            
        }
    }
}
