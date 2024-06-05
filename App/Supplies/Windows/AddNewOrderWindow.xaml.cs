using Supplies.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        Orders _currentOrder = new Orders();
        Ordered_components _orderedComponents = new Ordered_components();
        List<Components> selectedComponents = new List<Components>();

        int fullPrice = 1500;
        long orderCode;
        public AddNewOrderWindow(Orders order ,string checkCode)
        {
            InitializeComponent();

            if (checkCode != null)
            {
                _currentOrder = order;
                long checkCodeL = Convert.ToInt64(checkCode);
                orderCode = checkCodeL;
                var components = SuppliesDBEntities.GetContext().Ordered_components.Where(c => c.checkCode == checkCodeL).ToList();

                foreach (var component in components)
                {
                    var comp = SuppliesDBEntities.GetContext().Components.FirstOrDefault(i => i.ID == component.components_ID);
                    selectedComponents.Add(comp);
                    fullPrice += comp.price;
                }

                Update_Table();
            }

            fullNCB.ItemsSource = SuppliesDBEntities.GetContext().Clients.ToList();
            compTypeCB.ItemsSource = SuppliesDBEntities.GetContext().Components_type.ToList();
            compCB.ItemsSource = SuppliesDBEntities.GetContext().Components.ToList();

            DataContext = _currentOrder;
        }

        private void Item_Changed(object sender, SelectionChangedEventArgs e)
        {
            var tt = SuppliesDBEntities.GetContext().Components.ToList();

            if (compTypeCB.SelectedIndex > -1)
            {
                tt = tt.Where(t => t.type_ID == (compTypeCB.SelectedItem as Components_type).ID).ToList();
            }
            compCB.ItemsSource = tt;
        }

        private void Selected_Item(object sender, SelectionChangedEventArgs e)
        {
            if (compCB.SelectedValue as Components != null)
            {
                selectedComponents.Add(compCB.SelectedValue as Components);

                fullPrice = fullPrice + (compCB.SelectedValue as Components).price;

                compCB.SelectedIndex = -1;

                Update_Table();
            }
        }

        private void Dell_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Components>().ToList();

                foreach (var remove in Removing)
                {
                    selectedComponents.Remove(remove);
                }

                Update_Table();
            }
            else
            {
                MessageBox.Show("Выберите удаляемый компонент", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Update_Table()
        {
            DGrid.ItemsSource = selectedComponents.ToList();
            fullPriceTB.Text = Convert.ToString(fullPrice);
        }

        private void Add_Order(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();
            if (fullNCB.SelectedValue == null)
                Errors.AppendLine("Выберите клиента!");
            if (selectedComponents.Count == 0)
                Errors.AppendLine("Нельзя создать пустой заказ!");

            if (Errors.Length != 0)
            {
                MessageBox.Show(Errors.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int deliveryDate = 0;

            for (int i = 0; i < selectedComponents.Count; i++)
            {
                if (deliveryDate < selectedComponents[i].Supplies.deliveryTime)
                    deliveryDate = selectedComponents[i].Supplies.deliveryTime;
            }

            _currentOrder.deliveryDate = DateTime.Now.AddDays(deliveryDate);
            _currentOrder.fullPrice = fullPrice + 1500;
            
            if (_currentOrder.ID == 0)
            {
                _currentOrder.createDate = DateTime.Now;
                _currentOrder.orderStatus = 0;
                SuppliesDBEntities.GetContext().Orders.Add(_currentOrder);
            }
            
            try
            {
                SuppliesDBEntities.GetContext().SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }

            string code = Convert.ToString(_currentOrder.ID) + DateTime.Now.ToString("ddMMyyyyHHmm");
            int order_ID = _currentOrder.ID;

            var check = SuppliesDBEntities.GetContext().Ordered_components.Where(c => c.checkCode == orderCode);

            if (check != null)
            {
                foreach (var che in check)
                {
                    SuppliesDBEntities.GetContext().Ordered_components.Remove(che);
                }
            }

            for (int i = 0; i < selectedComponents.Count; i++)
            {

                _orderedComponents.components_ID = selectedComponents[i].ID;
                _orderedComponents.orders_ID = order_ID;
                if (orderCode != 0)
                {
                    _orderedComponents.checkCode = orderCode;
                }
                else
                {
                    _orderedComponents.checkCode = Convert.ToInt64(code);
                }

                SuppliesDBEntities.GetContext().Ordered_components.Add(_orderedComponents);

                try
                {
                    SuppliesDBEntities.GetContext().SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
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

            MessageBox.Show("Заказ сохранён", "Успех", MessageBoxButton.OK);
            Close();
        }
    }
}
