using System;
using System.Collections.Generic;
using System.Text;

namespace StockMarket
{
    public class Company
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public int Shares { get; set; }
        public Company(string Name, double Value, int Shares)
        {
            this.Name = Name;
            this.Value = Value;
            this.Shares = Shares;
        }
    }
}
