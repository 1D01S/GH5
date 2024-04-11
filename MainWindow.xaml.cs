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
using System.Data.SqlClient;
using GH5;

namespace GH5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchToAuthPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("AuthPage.xaml", UriKind.Relative));
        }

        private void SwitchToPage1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
    }
}