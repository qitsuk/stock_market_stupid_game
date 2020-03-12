using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CompanyNameGenerator cng;
        private Player p;
        public MainWindow()
        {
            InitializeComponent();
            cng = new CompanyNameGenerator();
            p = new Player(1000, 500, 100);
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void how_to_button_Click(object sender, RoutedEventArgs e)
        {
            HowToWindow htw = new HowToWindow();
            htw.ShowDialog();
        }

        private void new_game_button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow GW = new GameWindow(cng, p);
            Close();
            GW.Show();
        }
    }
}
