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
    /// Логика взаимодействия для OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        List<Components> selectedComponents = new List<Components>();
        public OrderDetailsWindow(string checkCode)
        {
            InitializeComponent();

            long checkCodeL = Convert.ToInt64(checkCode);

            var components = SuppliesDBEntities.GetContext().Ordered_components.Where(c => c.checkCode == checkCodeL).ToList();

            foreach (var component in components)
            {
                selectedComponents.Add(SuppliesDBEntities.GetContext().Components.FirstOrDefault(i => i.ID == component.components_ID));
            }

            DGrid.ItemsSource = selectedComponents.ToList();
        }
    }
}
