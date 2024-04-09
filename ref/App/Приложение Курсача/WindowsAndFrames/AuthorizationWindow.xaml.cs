using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Threading;
using Приложение_Курсача;

namespace LaborExchange
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        private static List<dynamic> userDataList = new List<dynamic>();

        string code;
        /* Инициализация всех компонентов */
        public AuthorizationWindow()
        {
            InitializeComponent();

            string connectionString = @"data source=LOCALHOST\SQLEXPRESS; database=LaborExchange; integrated security=SSPI";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetDecryptLoginAndPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var userData = new
                        {
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString(),
                            Access = reader["Access"]
                        };

                        userDataList.Add(userData);
                    }

                    reader.Close();
                }
            }
        }

        /* Глобальная переменная для разграничения прав */
        public static class Globals
        {
            public static bool Access;
        }

        /* Метод генерации кода доступа, запуск таймера, разблокирование поля для ввода кода и кнопки */
        private void gencode()
        {
            code = null;
            Random random = new Random();
            string[] massiveCharacters = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            for (int i = 0; i < 4; i++)
            {
                code += massiveCharacters[random.Next(0, massiveCharacters.Length)];
            }
            CodeBlock.Text = code;
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            CodeBox.IsEnabled = true;
            EnterBtn.IsEnabled = true;
            RefreshBtn.IsEnabled = true;
        }

        /* Метод остановки таймера при его окончании */
        void Timer_Tick(object sender, EventArgs e)
        {
            code = null;
            MessageBox.Show("Время написания кода вышло. Повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            timer.Stop();
            gencode();
        }

        /* Обработчик нажатия на ENTER для перехода к полю ввода пароля */
        private void LogBlock_Up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var db = new LaborExchangeEntities())
                {
                    var login = userDataList.FirstOrDefault(l => l.Login == LogTb.Text);

                    if (login == null)
                    {
                        MessageBox.Show("Неверный логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PassB.IsEnabled = true;
                        LogTb.IsEnabled = false;
                        PassB.Focus();
                    }
                }
            }
        }

        /* Обработчик нажатия на ENTER для перехода к полю ввода кода */
        private void PassBlock_Up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (var db = new LaborExchangeEntities())
                {
                    var login = userDataList.FirstOrDefault(l => l.Login == LogTb.Text & l.Password == PassB.Password);
                    if (login == null)
                    {
                        MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PassB.IsEnabled = false;
                        gencode();
                        CodeBox.Focus();
                    }
                }
            }
        }

        /* Обработчик нажатия на ENTER для перехода к основному окну */
        private void CodeBlock_Up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Entering(EnterBtn, e);
            }
        }

        /* Обработчик кнопки для проверки введённых данных и перехода к осному окну */
        private void Entering(object sender, RoutedEventArgs e)
        {
            using (var db = new LaborExchangeEntities())
            {
                var auth = userDataList.FirstOrDefault(m => m.Login == LogTb.Text && m.Password == PassB.Password);
                if (auth != null & code == CodeBox.Text)
                {
                    timer.Stop();
                    Globals.Access = auth.Access;
                    if (Globals.Access == true)
                    {
                        MessageBox.Show("Вы вошли с правами Администратора");
                    }
                    else
                    {
                        MessageBox.Show("Вы вошли с правами Специалиста");
                    }
                    WorkerWindow Nwin = new WorkerWindow();
                    Nwin.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверно написанн код, повторите снова!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CodeBox.Clear();
                    timer.Stop();
                    gencode();
                }
            }
        }

        /* Обработчик кнопки для перегенерации кода */
        private void Refresh(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            gencode();
            CodeBox.Focus();
        }

        /* Обработчик кнопки для очистки введённых данных */
        private void Cancel_Btm(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            LogTb.Clear();
            PassB.Clear();
            CodeBox.Clear();
            CodeBlock.Text = "";
            LogTb.IsEnabled = true;
            PassB.IsEnabled = false;
            CodeBox.IsEnabled = false;
            EnterBtn.IsEnabled = false;
            RefreshBtn.IsEnabled = false;
        }
    }
}
