using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockMarket
{
    /// <summary>
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        private Company c;
        private Player p;
        private GameWindow gw;
        private readonly List<Company> currentMarket;
        private Company companyToSell;
        public SellWindow(Company c, Player p, GameWindow gw, List<Company> currentMarket)
        {
            InitializeComponent();
            this.c = c;
            this.p = p;
            this.gw = gw;
            this.currentMarket = currentMarket;
            Title = "Selling " + c.Name + " shares.";
            title_label.Content = "Selling shares of " + c.Name;
            setSellBox();
            companyToSell = findCompany(currentMarket, c);
            bought_at_label.Content = "Bought at: $" + c.Value;
            trading_at_label.Content = "Trading at: $" + companyToSell.Value;
        }

        private void sell_button_Click(object sender, RoutedEventArgs e)
        {
            int amountToSell;
            try
            {
                amountToSell = Convert.ToInt32(sell_textbox.Text);
                if (amountToSell > c.Shares)
                {
                    MessageBox.Show("Nice try! You don't have that many shares. You can sell what you have, which is " + c.Shares + ".", "Can't sell something you don't have.");
                    setSellBox();
                }
                else if (amountToSell == 0 || amountToSell < 0)
                {
                    MessageBox.Show("You can't sell nothing or less. Wanna try that again?", "Can't sell nothing!");
                    setSellBox();
                }
                else
                {
                    MessageBoxResult result = MessageBoxResult.None;
                    double margin = calculateMargin(companyToSell.Value, amountToSell, c.Value);
                    if (margin > c.Value)
                    {
                        result = MessageBox.Show("Are you sure you want to sell " + amountToSell + " " + c.Name + " shares?\nThis will net you $" + Math.Round(c.Shares * companyToSell.Value, 2, MidpointRounding.AwayFromZero) + ". You will earn: $" + margin + " pr. share.", "Are you sure?", MessageBoxButton.YesNo);
                    }
                    else if (margin == 0)
                    {
                        result = MessageBox.Show("Are you sure you want to sell " + amountToSell + " " + c.Name + " shares?\nThis will net you $" + Math.Round(c.Shares * companyToSell.Value, 2, MidpointRounding.AwayFromZero) + ". You will break even on this trade.", "Are you sure?", MessageBoxButton.YesNo);
                    }
                    else if (margin < c.Value)
                    {
                        result = MessageBox.Show("Are you sure you want to sell " + amountToSell + " " + c.Name + " shares?\nThis will net you $" + Math.Round(c.Shares * companyToSell.Value, 2, MidpointRounding.AwayFromZero) + ". You will lose: $" + margin + " pr. share.", "Are you sure?", MessageBoxButton.YesNo);
                    }
                    if (result == MessageBoxResult.No) { }
                    else if (result == MessageBoxResult.Yes)
                    {
                        p.Balance += (companyToSell.Value * c.Shares);
                        p.SellShares(c, amountToSell);
                        gw.UpdateAll(false);
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("I said only whole numbers!! Try again!", "That's not a number!!");
                Console.WriteLine(ex.ToString());
            }
        }

        private Company findCompany(List<Company> market, Company toSell)
        {
            var itemIndex = market.FindIndex(x => x.Name == toSell.Name);
            Company match = null;
            if (itemIndex != -1)
            {
                match = market.ElementAt(itemIndex);
            }
            return match;
        }

        private double calculateMargin(double currentValue, int amount, double oldValue)
        {
            return Math.Round(((currentValue * amount) - (oldValue * amount)) / amount, 2, MidpointRounding.AwayFromZero);
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void setSellBox()
        {
            sell_textbox.Text = c.Shares.ToString();
        }
    }
}
