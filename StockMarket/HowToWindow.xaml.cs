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
    /// Interaction logic for HowToWindow.xaml
    /// </summary>
    public partial class HowToWindow : Window
    {
        public HowToWindow()
        {
            InitializeComponent();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
