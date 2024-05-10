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
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        Orders _currentOrder = new Orders();

        List<Components> selectedComponents = new List<Components>();

        public AddNewOrderWindow(Orders selectedOrder)
        {
            InitializeComponent();

            if (selectedOrder != null)
            {
                _currentOrder = selectedOrder;
            }

            fullNCB.ItemsSource = SuppliesDBEntities.GetContext().Clients.ToList();
            compTypeCB.ItemsSource = SuppliesDBEntities.GetContext().Components_type.ToList();
            compCB.ItemsSource = SuppliesDBEntities.GetContext().Components.ToList();

            DataContext = _currentOrder;
        }

        private void Item_Changed(object sender, SelectionChangedEventArgs e)
        {
            var tt = SuppliesDBEntities.GetContext().Components.ToList();

            if (compTypeCB.SelectedIndex > 0)
            {
                tt = tt.Where(t => t.type_ID == (compTypeCB.SelectedItem as Components_type).ID).ToList();
            }

            compCB.ItemsSource = tt;
        }

        private void Selected_Item(object sender, SelectionChangedEventArgs e)
        {
            selectedComponents.Add(compCB.SelectedValue as Components);

            Update_Table();
        }

        private void Update_Table()
        {
            DGrid.ItemsSource = selectedComponents.ToList();
        }
    }
}
