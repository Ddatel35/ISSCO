using Supplies.Database;
using Supplies.Windows;
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

namespace Supplies
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Entering(object sender, RoutedEventArgs e)
        {
            var auth = SuppliesDBEntities.GetContext().Users.AsNoTracking().FirstOrDefault(a => a.login == loginTxb.Text && a.password == passBox.Password);
            if (auth != null)
            {
                MessageBox.Show("Добро пожаловать!", "Приветствие", MessageBoxButton.OK);
                WorkerWindow WW = new WorkerWindow();
                WW.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Clean(object sender, RoutedEventArgs e)
        {
            loginTxb.Text = string.Empty;
            passBox.Password = string.Empty;
        }
    }
}
