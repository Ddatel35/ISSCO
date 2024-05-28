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
    /// Логика взаимодействия для AddNewComponentWindow.xaml
    /// </summary>
    public partial class AddNewComponentWindow : Window
    {
        Components _currenteComponents = new Components();
        public AddNewComponentWindow(Components selectedComponents)
        {
            InitializeComponent();
            if (selectedComponents != null)
            {
                _currenteComponents = selectedComponents;
                AddBtn.Content = "Сохранить";
                titleH.Text = "Редактирование комплектующего";
            }

            DataContext = _currenteComponents;

            CompTCB.ItemsSource = SuppliesDBEntities.GetContext().Components_type.ToList();
            SuppCB.ItemsSource = SuppliesDBEntities.GetContext().Supplies.ToList();
        }

        private void Add_Client(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (CompTCB.SelectedValue == null)
                Errors.AppendLine("Укажите тип комплектующего!");
            if (SuppCB.SelectedValue == null)
                Errors.AppendLine("Укажите поставщика!");
            if (NameTB.Text == "")
                Errors.AppendLine("Укажите название комплектующего!");
            if (PriceTB.Text == "")
                Errors.AppendLine("Укажите ценну комплектующего!");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString(), "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (_currenteComponents.ID == 0)
            {
                SuppliesDBEntities.GetContext().Components.Add(_currenteComponents);
            }

            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Комплектующее сохранена");
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
