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
using System.IO;


namespace Supplies.Frames
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            IntToStringConv converter = new IntToStringConv();
            Resources.Add("IntToStringConv", converter);
            UpdateTable();
        }

        private void UpdateTable()
        {
            DGrid.ItemsSource = SuppliesDBEntities.GetContext().Orders.ToList();
        }

        private void Add_Order(object sender, RoutedEventArgs e)
        {
            AddNewOrderWindow ANOW = new AddNewOrderWindow(null);
            ANOW.ShowDialog();
            UpdateTable();
        }

        private void Edit_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewOrderWindow ANOW = new AddNewOrderWindow(DGrid.SelectedItem as Orders);
                ANOW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите заказ который хотите изменить!");

            UpdateTable();
        }

        private void Del_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Orders>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        foreach (var rem in Removing)
                        {
                            string checkCode = Convert.ToInt64(rem.ID) + rem.createDate.ToString().Remove(2, 1).Remove(4, 1).Remove(8, 1).Remove(10, 1).Remove(12);

                            long checkCodeL = Convert.ToInt64(checkCode);

                            var ordered = SuppliesDBEntities.GetContext().Ordered_components.Where(x => x.checkCode == checkCodeL);

                            foreach (var order in ordered)
                            {
                                SuppliesDBEntities.GetContext().Ordered_components.Remove(order);
                            }

                            SuppliesDBEntities.GetContext().Orders.Remove(rem);
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
                MessageBox.Show("Выберите удаляемый заказ!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
