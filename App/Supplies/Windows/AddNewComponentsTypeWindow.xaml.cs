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
    /// Логика взаимодействия для AddNewComponentsTypeWindow.xaml
    /// </summary>
    public partial class AddNewComponentsTypeWindow : Window
    {
        Components_type _currentComponentsType = new Components_type();
        public AddNewComponentsTypeWindow()
        {
            InitializeComponent();
            DataContext = _currentComponentsType;
        }

        private void AddComponentsType(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (NameTB.Text == "")
                Errors.AppendLine("Укажите тип комплектующего!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currentComponentsType.ID == 0)
            {
                SuppliesDBEntities.GetContext().Components_type.Add(_currentComponentsType);
            }

            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Тип комплектующего сохранён");
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
