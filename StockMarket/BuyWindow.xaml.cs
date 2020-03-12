using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
    /// Interaction logic for BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        private Company c;
        private Player p;
        GameWindow gw;
        public BuyWindow(Company c, Player p, GameWindow gw)
        {
            InitializeComponent();
            this.c = c;
            this.p = p;
            this.gw = gw;
            Title = "Buying shares of " + c.Name;
            title_label.Content = "Buying shares of " + c.Name;
            setBuyBox();
            
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buy_button_Click(object sender, RoutedEventArgs e)
        {
            int amountToBuy;
            try
            {
                amountToBuy = Convert.ToInt32(buy_text.Text);
                if (amountToBuy > c.Shares)
                {
                    MessageBox.Show("There aren't that many shares available!", "Not Enough Shares!");
                    setBuyBox();
                }
                else if (amountToBuy == 0 || amountToBuy < 0)
                {
                    MessageBox.Show("You can't buy 0 or less shares.", "Can't buy nothing.");
                    setBuyBox();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to buy " + amountToBuy + "\n" + c.Name + " shares?\nThis will cost you $" + Math.Round((amountToBuy * c.Value),MidpointRounding.AwayFromZero), "Confirm purchase of " + amountToBuy + " shares", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No) {}
                    else if(result == MessageBoxResult.Yes)
                    {
                        if (p.Balance < amountToBuy * c.Value)
                        {
                            MessageBox.Show("You don't have enough money to buy that many shares!", "Not enough moola!");
                            setBuyBox();
                        }
                        else
                        {
                            p.Balance -= amountToBuy * c.Value;
                            c.Shares -= amountToBuy;
                            MessageBox.Show("Succesfully purchased " + amountToBuy + " " + c.Name + " shares.\nThey have been added to your portfolio.");
                            p.AddToPortfolio(new Company(c.Name, c.Value, amountToBuy));
                            gw.UpdateAll(false);
                            Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("I said only whole numbers!! Try again!", "That's not a number!!");
                Console.WriteLine(ex.ToString());
            }
        }
        private void setBuyBox()
        {
            int canAfford = Convert.ToInt32(Math.Floor(p.Balance / c.Value));
            if (c.Shares < canAfford)
            {
                buy_text.Text = c.Shares.ToString();
            }
            else
            {
                buy_text.Text = canAfford.ToString();
            }
        }
    }
}
