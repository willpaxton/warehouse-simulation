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
        public string driverFirstName;
        public string driverLastName;
        public string deliveryCompany; 
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
            
            string line = File.ReadLines("E:\\ETSU\\warehouse-simulation\\warehouse-project\\data\\driverData.csv").Skip(random.Next(0,1000)).Take(1).First();

            string[] data = line.Split(",");
            driverFirstName = data[0];
            driverLastName = data[1];
            deliveryCompany = data[2];
            

            //Console.WriteLine(line);
            
        }
    }
}
