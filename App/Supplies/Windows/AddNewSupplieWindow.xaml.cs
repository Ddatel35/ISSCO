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
    /// Логика взаимодействия для AddNewSupplieWindow.xaml
    /// </summary>
    public partial class AddNewSupplieWindow : Window
    {
        SuppliesT _currentSupply = new SuppliesT();
        public AddNewSupplieWindow(SuppliesT currentSupply)
        {
            InitializeComponent();
            if (currentSupply != null)
            {
                _currentSupply = currentSupply;
                AddBtn.Content = "Сохранить";
                titleH.Text = "Редактирование поставщика";
            }
            DataContext = _currentSupply;
        }

        private void Add_Client(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (FullNTB.Text == "")
                Errors.AppendLine("Укажите название компании!");
            if (AddresTB.Text == "")
                Errors.AppendLine("Укажите адрес поставщика!");
            if (PhoneTB.Text == "")
                Errors.AppendLine("Укажите номер телефона поставщика!");
            if (DelivTTB.Text == "")
                Errors.AppendLine("Укажите количество дней доставки!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentSupply.ID == 0)
            {
                SuppliesDBEntities.GetContext().Supplies.Add(_currentSupply);
            }

            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Поставщик сохранён", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
