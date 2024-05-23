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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            DGrid.ItemsSource = SuppliesDBEntities.GetContext().Clients.ToList();
        }

        private void Add_Client(object sender, RoutedEventArgs e)
        {
            AddNewClientWindow ANCW = new AddNewClientWindow(null);
            ANCW.ShowDialog();
            UpdateTable();
        }

        private void Edit_Client(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewClientWindow ANCW = new AddNewClientWindow(DGrid.SelectedItem as Clients);
                ANCW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите редактируемого клиенита!");

            UpdateTable();
        }

        private void Del_Client(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Clients>().ToList();

                foreach (var check in Removing)
                {
                    if (SuppliesDBEntities.GetContext().Orders.FirstOrDefault(n => n.client_ID == check.ID) != null)
                    {
                        MessageBox.Show($"В заказах указывается клиент с именнем {check.fullName}, для удаление очистите записи данного клиента!", 
                                        "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in Removing)
                        {
                            SuppliesDBEntities.GetContext().Clients.Remove(rem);
                        }
                        SuppliesDBEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удаленны");
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
                MessageBox.Show("Выберите удаляемоого руководителя!", "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
