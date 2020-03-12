using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private CompanyNameGenerator cng;
        private Player p;
        private List<Company> companies;
        private int currentDay = 1;

        public GameWindow(CompanyNameGenerator cng, Player p)
        {
            InitializeComponent();
            this.cng = cng;
            this.p = p;
            companies = cng.GetGeneratedCompanies();
            market_datagrid.ItemsSource = companies;
            player_balance_label.Content += p.Balance.ToString();
            current_day_label.Content = "You are on day " + currentDay + "/30";
        }

        private void market_datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCompany = market_datagrid.SelectedItem as Company;
            if (selectedCompany == null) return;
            if(selectedCompany.Shares == 0)
            {
                MessageBox.Show("There are currently no shares for sale. Try again tomorrow.", "No shares for sale");
            }
            else
            {
                BuyWindow bw = new BuyWindow(selectedCompany, p, this);
                bw.ShowDialog();
            }
        }

        private void portfolio_datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCompany = portfolio_datagrid.SelectedItem as Company;
            if (selectedCompany == null) return;
            SellWindow sw = new SellWindow(selectedCompany, p, this, companies);
            sw.ShowDialog();
        }

        public void UpdateAll(bool nextDay)
        {
            player_balance_label.Content = "Your Liquid Funds: $ " + Math.Round(p.Balance, 2, MidpointRounding.AwayFromZero);
            current_day_label.Content = "You are on day " + currentDay + "/30";
            market_datagrid.ItemsSource = null;
            market_datagrid.ItemsSource = companies;
            portfolio_datagrid.ItemsSource = null;
            portfolio_datagrid.ItemsSource = p.Portfolio;
            if (nextDay) event_box_label1.Content = sleep() + "\n" + dailyExpences(p.Rent, p.Food);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wanna quit now?", "Are ya a quitter?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void next_day_button_Click(object sender, RoutedEventArgs e)
        {
            currentDay++;
            companies = cng.UpdateSharePrice(companies);
            p.Balance -= p.DailyExpences;
            UpdateAll(true);
        }

        private string sleep()
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

        private string dailyExpences(double rent, double food)
        {
            double total = rent + food;
            return "You paid your daily rent of $" + rent + " and put aside the $" + food + ", you need to eat. \nA total of $" + total + " has been deducted from your account.";
        }
    }
}
