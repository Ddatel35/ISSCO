using Supplies.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

namespace Supplies.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        Clients _currentClient = new Clients();
        public AddNewClientWindow(Clients selectedClient)
        {
            InitializeComponent();

            if (selectedClient != null)
            {
                _currentClient = selectedClient;
                AddBtn.Content = "Сохранить";
            }

            DataContext = _currentClient;
        }

        private void Add_Client(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (FullNTB.Text == "")
                Errors.AppendLine("Укажите тип комплектующего!");
            if (AddresTB.Text == "")
                Errors.AppendLine("Укажите тип комплектующего!");
            if (PhoneTB.Text == "")
                Errors.AppendLine("Укажите тип комплектующего!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentClient.ID == 0)
            {
                SuppliesDBEntities.GetContext().Clients.Add(_currentClient);
            }

            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Клиент сохранён");
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());

                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }
        }
    }
}
