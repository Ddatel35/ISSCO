using System;
using System.Collections.Generic;
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
using Приложение_Курсача;

namespace LaborExchange.WindowsAndFrames
{
    /// <summary>
    /// Логика взаимодействия для AddCitizenWindow.xaml
    /// </summary>
    public partial class AddCitizenWindow : Window
    {
        private Citizen _currentCitizen = new Citizen();

        /* Инициализация всех компонентов и проверка на необходимое действие */
        public AddCitizenWindow(Citizen selectedCitezen)
        {
            InitializeComponent();

            if (selectedCitezen != null)
            {
                _currentCitizen = selectedCitezen;
                AddEdit_Btn.Content = "Сохранить";
            }

            DataContext = _currentCitizen;
        }

        /* Обработчик кнопки для сохранения иди добавление гражданина */
        private void AddNewCit_Btn(object sender, RoutedEventArgs e)
        {
            StringBuilder Errors = new StringBuilder();

            if (FioCit.Text == "")
                Errors.AppendLine("Введите ФИО");
            if (EdicaCit.Text == "")
                Errors.AppendLine("Введите образование гражданина");
            if (PhoneCit.Text == "")
                Errors.AppendLine("Введите номер телефона");
            if (BirCit.SelectedDate == null)
                Errors.AppendLine("Введите день рождения");

            if (Errors.Length > 0)
            {
                MessageBox.Show(Errors.ToString());
                return;
            }

            if (_currentCitizen.Id == 0)
            {
                LaborExchangeEntities.GetContext().Citizen.Add(_currentCitizen);
            }

            try
            {
                LaborExchangeEntities.GetContext().SaveChanges();
                Close();
                MessageBox.Show("Гражданин сохранён");
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
