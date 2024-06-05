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

namespace Supplies.Frames
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        void UpdateTable()
        {
            DGrid.ItemsSource = SuppliesDBEntities.GetContext().Users.ToList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        {
            AddNewUserWindow ANUW = new AddNewUserWindow(null);
            ANUW.ShowDialog();
            UpdateTable();
        }

        private void Edit_User(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewUserWindow ANUW = new AddNewUserWindow(DGrid.SelectedItem as Users);
                ANUW.ShowDialog();
                UpdateTable();
            }
            else
            {
                MessageBox.Show("Выберите редактируемого пользователя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Del_User(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var removing = DGrid.SelectedItems.Cast<Users>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in removing)
                        {
                            SuppliesDBEntities.GetContext().Users.Remove(rem);
                        }
                        SuppliesDBEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удаленны", "Успех", MessageBoxButton.OK);
                        UpdateTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите удаляемого пользователя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
