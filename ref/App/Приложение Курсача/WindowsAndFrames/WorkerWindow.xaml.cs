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
using Приложение_Курсача.WindowsAndFrames;

namespace LaborExchange
{
    /// <summary>
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        /* Инициализация всех компонентов и вывод первого фрейма */
        public WorkerWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new VacanciesPage());
            Manager.MainFrame = MainFrame;
        }

        /* Обработчик кнопки для перехода к окну авторизации */
        private void Exit(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow MW = new AuthorizationWindow();
            MW.Show();
            Close();
        }

        /* Обработчик кнопоки для перемещению к фрейму отчётов */
        private void ReportPage_Btn(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReportPage());
            Back.Visibility = Visibility.Visible;
        }

        /* Обработчик кнопки для возращения к предыдущему фрейму */
        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
            Back.Visibility = Visibility.Hidden;
        }
    }
}
