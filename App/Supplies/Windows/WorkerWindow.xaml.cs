using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Supplies.Frames;

namespace Supplies.Windows
{
    /// <summary>
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public WorkerWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new OrdersPage());
            Manager.MainFrame = MainFrame;
        }

        private void GoTo_Clients(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientsPage());
        }

        private void GoTo_Orders(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage());
        }

        private void GoTo_Components(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ComponentsPage());
        }

        private void GoTo_Supplies(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SuppliesPage());
        }
    }
}
