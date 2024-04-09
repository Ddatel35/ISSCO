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
using LaborExchange.WindowsAndFrames;
using Приложение_Курсача;

namespace LaborExchange
{
    /// <summary>
    /// Логика взаимодействия для InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {
        LaborExchangeEntities db = new LaborExchangeEntities();

        /* Инициализация всех компонентов и разограничения прав с выводом граждан из БД */
        public InformationPage()
        {
            InitializeComponent();
            
            if (AuthorizationWindow.Globals.Access == true)
            {
                AddCit.Visibility = Visibility.Hidden;
                DelCit.Visibility = Visibility.Hidden;
                EditCit.Visibility = Visibility.Hidden;
            }
            DGrid.ItemsSource = db.Citizen.ToList();
        }

        /* Обработчик кнопки для возращения к предыдущему фрейму */
        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        /* Обработчик кнопки для открытия окна добавления гражданина в БД */
        private void AddCit_Btn(object sender, RoutedEventArgs e)
        {
            AddCitizenWindow AddCitizenWindow = new AddCitizenWindow(null);
            AddCitizenWindow.ShowDialog();
            DGrid.ItemsSource = db.Citizen.ToList();
        }

        /* Обработчик кнопки для удаления выбранного гражданина из БД */
        private void DelCit_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Citizen>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Citizen.RemoveRange(Removing);
                        db.SaveChanges();
                        MessageBox.Show("Данные удаленны");
                        DGrid.ItemsSource = db.Citizen.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите гражднанина которого хотите удалить", "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /* Обработчик кнопки для открытия окна редактирования выбранного гражднина из БД */
        private void EditCit_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddCitizenWindow edit = new AddCitizenWindow(DGrid.SelectedItem as Citizen);
                edit.ShowDialog();
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Выберите гражданина");
            }
            DGrid.ItemsSource = db.Citizen.ToList();
        }
    }
}
