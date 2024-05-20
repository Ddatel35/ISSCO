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
    /// Логика взаимодействия для SupplesOrderInfoWindow.xaml
    /// </summary>
    public partial class SupplesOrderInfoWindow : Window
    {
        List<Components> selectedComponents = new List<Components>();

        int ID;
        public SupplesOrderInfoWindow(string checkCode, int order_ID)
        {
            InitializeComponent();

            ID = order_ID;
            long checkCodeL = Convert.ToInt64(checkCode);
            var components = SuppliesDBEntities.GetContext().Ordered_components.Where(c => c.checkCode == checkCodeL).ToList();

            foreach (var component in components)
            {
                selectedComponents.Add(SuppliesDBEntities.GetContext().Components.FirstOrDefault(i => i.ID == component.components_ID));
            }

            DGrid.ItemsSource = selectedComponents.ToList();
        }

        private void Change_Order_Status(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверенны?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Orders order = SuppliesDBEntities.GetContext().Orders.Find(ID);
                order.orderStatus = 1;

                try
                {
                    SuppliesDBEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                MessageBox.Show("Статус заказа сменён", "Успех", MessageBoxButton.OK);
            }
        }
    }
}
