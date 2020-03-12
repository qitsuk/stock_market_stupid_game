using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockMarket
{
    public class Player
    {
        public double Balance { get; set; }
        public List<Company> Portfolio { get; set; }
        public double Rent { get; set; }
        public double Food { set; get; }
        public double DailyExpences { get; set; }

        public Player(double balance, double rent, double food)
        {
            Balance = balance;
            Food = food;
            Rent = rent;
            DailyExpences = Rent + Food;
            Portfolio = new List<Company>();
        }

        public void AddToPortfolio(Company c)
        {
            var itemIndex = Portfolio.FindIndex(x => x.Name == c.Name);
            if (itemIndex != -1) 
            { 
                var company = Portfolio.ElementAt(itemIndex);
                int currentShares = company.Shares;
                double currentValue = company.Value;
                int newShares = c.Shares;
                double newPrice = c.Value;
                company.Shares += c.Shares;
                company.Value = Math.Round(calcNewValue(currentValue, newPrice, currentShares, newShares, (currentShares + newShares)), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                Portfolio.Add(c);
            }
        }

        private double calcNewValue(double originValue, double newValue, int originShares, int newShares, int totalShares)
        {
            return ((originShares * originValue) + (newShares * newValue)) / totalShares;
        }

        public void SellShares(Company c, int amountToSell)
        {
            var itemIndex = Portfolio.FindIndex(x => x.Name == c.Name);
            int currentShares = Portfolio.ElementAt(itemIndex).Shares;
            if (currentShares == amountToSell)
            {
                Portfolio.RemoveAt(itemIndex);
            }
            else
            {
                Portfolio.ElementAt(itemIndex).Shares -= amountToSell;
            }
        }
    }
}
