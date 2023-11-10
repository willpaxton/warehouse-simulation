using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_project
{
    public class Crate
    {
        private string id;
        private double price;

        public Crate()
        {
            this.price = GetPrice();
        }

        private double GetPrice()
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
