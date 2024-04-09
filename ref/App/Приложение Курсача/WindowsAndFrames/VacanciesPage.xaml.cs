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
    /// Логика взаимодействия для Vacancies.xaml
    /// </summary>
    public partial class VacanciesPage : Page
    {
        LaborExchangeEntities db = new LaborExchangeEntities();

        /* Инициализация всех компонентов и разограничения прав с выводом данных */
        public VacanciesPage()
        {
            InitializeComponent();
            if (AuthorizationWindow.Globals.Access == false)
            {
                AddVac.Visibility = Visibility.Hidden;
                DelVac.Visibility = Visibility.Hidden;
                EditVac.Visibility = Visibility.Hidden;
            }
            else
            {
                ChoVac.Visibility = Visibility.Hidden;
            }
            UpdateTable();
        }

        /* Метод вывода открытых вакансий из БД */
        private void UpdateTable()
        {
            var list = from vac in db.Vacancies where vac.Status == true select vac;
            DGrid.ItemsSource = list.ToList();
        }

        /* Обработчик кнопки для перемещения по фреймам */
        private void InfFrame(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new InformationPage());
        }

        /* Обработчик кнопки для перемещения к окну добавления вакансии в БД */
        private void AddVac_Btn(object sender, RoutedEventArgs e)
        {
            AddVacanciesWindow AVac = new AddVacanciesWindow(null);
            AVac.ShowDialog();
            UpdateTable();
        }

        /* Обработчик кнопки для удаления выбранной вакансии из БД */
        private void DelVac_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Removing = DGrid.SelectedItems.Cast<Vacancies>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить следующие {Removing.Count()} элеметнов?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var rem in Removing)
                        {
                            db.Vacancies.Remove(rem);
                        }
                        db.SaveChanges();
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
                MessageBox.Show("Выберите удаляемую ваканисию вакансию", "Вниманеие", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /* Обработчик кнопки для выбора вакансии из таблицы */
        private void ChoVac_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                var Choose = DGrid.SelectedItems.Cast<Vacancies>().ToList();
                if (MessageBox.Show($"Вы точно хотите выбрать '{Choose.FirstOrDefault().Name}' ваканисию?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int IdVac = Choose.FirstOrDefault().Id;
                    try
                    {
                        Vacancies vacancies = db.Vacancies.Find(IdVac);
                        vacancies.NumberOfPlaces--;
                        int NOP = vacancies.NumberOfPlaces;
                        if (NOP <= 0)
                        {
                            vacancies.Status = false;
                            db.SaveChanges();
                        }
                        else
                        {
                            db.SaveChanges();
                        }
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
                MessageBox.Show("Выберите вакансию", "Вниманеие", MessageBoxButton.OK ,MessageBoxImage.Warning);
            }
        }

        /* Обработчик кнопки для перехода к окну редактирования */
        private void EditVac_Btn(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                AddVacanciesWindow edit = new AddVacanciesWindow(DGrid.SelectedItem as Vacancies);
                edit.ShowDialog();
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Выберите вакансию");
            }
            UpdateTable();
        }
    }
}
