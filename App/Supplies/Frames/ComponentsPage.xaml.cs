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
    /// Логика взаимодействия для ComponentsPage.xaml
    /// </summary>
    public partial class ComponentsPage : Page
    {
        public ComponentsPage()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            DGrid.ItemsSource = SuppliesDBEntities.GetContext().Components.ToList();
        }

        private void GoToComponentsType(object sender, RoutedEventArgs e)
        {
            ComponentsTypeWindow CTW = new ComponentsTypeWindow();
            CTW.Show();
        }

        private void Add_Components(object sender, RoutedEventArgs e)
        {
            AddNewComponentWindow ANCW = new AddNewComponentWindow(null);
            ANCW.ShowDialog();
            UpdateTable();
        }

        private void Edit_Components(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddNewComponentWindow ANCW = new AddNewComponentWindow(DGrid.SelectedItem as Components);
                ANCW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите редактируемый компонент!");

            UpdateTable();
        }

        private void Del_Components(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Components>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in Removing)
                        {
                            SuppliesDBEntities.GetContext().Components.Remove(rem);
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
                MessageBox.Show("Выберите редактируемый компонент!", "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
