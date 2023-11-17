using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
            Random random = new Random();
            
            string line = File.ReadLines("C:\\Users\\wpaxt\\Documents\\Projects\\warehouse-simulation\\warehouse-project\\data\\driverData.csv").Skip(random.Next(0,1000)).Take(1).First();

            Console.WriteLine(line);
            
        }
    }
}
