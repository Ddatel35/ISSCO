using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
using Приложение_Курсача;

namespace LaborExchange.WindowsAndFrames
{
    /// <summary>
    /// Логика взаимодействия для AddVacanciesWindow.xaml
    /// </summary>
    public partial class AddVacanciesWindow : Window
    {
        private Vacancies _currentVacancies = new Vacancies();

        /* Инициализация всех компонентов и проверка на необходимое действие */
        public AddVacanciesWindow(Vacancies selectedVacancies)
        {
            InitializeComponent();

            if (selectedVacancies != null)
            {
                _currentVacancies = selectedVacancies;
                AddEdit_Btn.Content = "Сохранить";
            }

            DataContext = _currentVacancies;
        }

        /* Обработчик кнопки для сохранения иди добавление вакансии */
        private void AddNewVac_Btn(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();

            if (NameVac.Text == "")
                Errors.AppendLine("Введите имя вакансии");
            if (EdicaVac.Text == "")
                Errors.AppendLine("Введите требуемое образование");
            if (NPlacesVac.Text == "")
                Errors.AppendLine("Введите количество мест");
            if (ScheVac.Text == "")
                Errors.AppendLine("Введите график работы");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString());
                return;
            }

            if (_currentVacancies.Id == 0)
            {
                LaborExchangeEntities.GetContext().Vacancies.Add(_currentVacancies);
            }

            try
            {
                LaborExchangeEntities.GetContext().SaveChanges();
                MessageBox.Show("Вакансия сохраненна");
                Close();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
