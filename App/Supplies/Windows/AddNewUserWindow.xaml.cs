using Supplies.Database;
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
using System.Windows.Shapes;

namespace Supplies.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        Users _currentUser = new Users();
        public AddNewUserWindow(Users selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
            {
                _currentUser = selectedUser;
                titleH.Text = "Редактирование пользователя";
                AddBtn.Content = "Сохранить";
            }

            DataContext = _currentUser;

            roleCB.ItemsSource = SuppliesDBEntities.GetContext().Roles.ToList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (loginTB.Text == "")
                Errors.AppendLine("Укажите логин пользователя!");
            if (passwordTB.Text == "")
                Errors.AppendLine("Укажите пароль пользователя!");
            if (roleCB.SelectedValue == null)
                Errors.AppendLine("Укажите роль пользователя!");

            if (Errors.Length != 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_currentUser.ID == 0)
            {
                SuppliesDBEntities.GetContext().Users.Add(_currentUser);
            }

            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь сохранён", "Успех", MessageBoxButton.OK);
                Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
