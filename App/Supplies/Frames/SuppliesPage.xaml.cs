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
    /// Логика взаимодействия для SuppliesPage.xaml
    /// </summary>
    public partial class SuppliesPage : Page
    {
        public SuppliesPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            DGrid.ItemsSource = SuppliesDBEntities.GetContext().Supplies.ToList();
        }

        private void Add_Supply(object sender, RoutedEventArgs e)
        {
            AddNewSupplieWindow ANSW = new AddNewSupplieWindow(null);
            ANSW.ShowDialog();
            UpdateTable();
        }

        private void Edit_Supply(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewSupplieWindow ANSW = new AddNewSupplieWindow(DGrid.SelectedItem as SuppliesT);
                ANSW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите удаляемого поставщика!");

            UpdateTable();
        }

        private void Del_Supply(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<SuppliesT>().ToList();

                foreach (var check in Removing)
                {
                    if (SuppliesDBEntities.GetContext().Components.FirstOrDefault(n => n.supplies_ID == check.ID) != null)
                    {
                        MessageBox.Show($"Товары существуют в базе данных от {check.name}, для удаления данных очистите эти товары!", 
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
                            SuppliesDBEntities.GetContext().Supplies.Remove(rem);
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
                MessageBox.Show("Выберите удаляемого поставщика!", "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
