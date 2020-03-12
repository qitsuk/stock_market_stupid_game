﻿using System;
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
        public string sleep()
        {
            Random rnd = new Random();
            int sleepRandom = rnd.Next(0, 3);
            if (sleepRandom == 0)
            {
                return "You slept through the night, without a care. You wake up refreshed and ready.";
            }
            else if (sleepRandom == 1)
            {
                return "You slept ok, but you had a fairly weird dream. You brush it of and get ready for the day.";
            }
            else if (sleepRandom == 2)
            {
                return "You slept like crap, and you're feeling kinda groggy. Take care with your investments today.";
            }
            return "You slept. Period.";
        }

        public string dailyExpences()
        {
            double total = Rent + Food;
            return "You paid your daily rent of $" + Rent + " and put aside the $" + Food + ", you need to eat. \nA total of $" + total + " has been deducted from your account.";
        }
    }
}
