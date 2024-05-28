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
using Word = Microsoft.Office.Interop.Word;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;
using System.Threading;

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

            if (MainWindow.Globals.Access == 2)
            {
                AddBtn.Visibility = Visibility.Hidden;
                AboBtn.Visibility = Visibility.Hidden;
                DelBtn.Visibility = Visibility.Hidden;
                OrdBtn.Visibility = Visibility.Hidden;

                InvBtn.Visibility = Visibility.Visible;
                DelivBtn.Visibility = Visibility.Visible;
            }

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
            AddNewOrderWindow ANOW = new AddNewOrderWindow();
            ANOW.ShowDialog();
            UpdateTable();
        }

        private void About_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                Orders order = DGrid.SelectedItem as Orders;
                DateTime date = DateTime.ParseExact(order.createDate.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                string formattedDate = date.ToString("ddMMyyyyHHmm");
                string checkCode = Convert.ToInt64(order.ID) + formattedDate;
                OrderDetailsWindow ODW = new OrderDetailsWindow(checkCode);
                ODW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите заказ, информацию о котором хотите посмотреть!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                            DateTime date = DateTime.ParseExact(rem.createDate.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            string formattedDate = date.ToString("ddMMyyyyHHmm");

                            string checkCode = Convert.ToInt64(rem.ID) + formattedDate;

                            long checkCodeL = Convert.ToInt64(checkCode);

                            var ordered = SuppliesDBEntities.GetContext().Ordered_components.Where(x => x.checkCode == checkCodeL);

                            foreach (var order in ordered)
                            {
                                SuppliesDBEntities.GetContext().Ordered_components.Remove(order);
                            }

                            SuppliesDBEntities.GetContext().Orders.Remove(rem);
                        }
                        SuppliesDBEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удаленны", "Успех", MessageBoxButton.OK);
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

        private void Supplies_Info_Order(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                Orders order = DGrid.SelectedItem as Orders;

                if (order.orderStatus != 0)
                {
                    MessageBox.Show("Статус заказа можно сменить только у заказов которые не заказанныx!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime date = DateTime.ParseExact(order.createDate.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                string formattedDate = date.ToString("ddMMyyyyHHmm");

                string checkCode = Convert.ToInt64(order.ID) + formattedDate;
                SupplesOrderInfoWindow SOIW = new SupplesOrderInfoWindow(checkCode, order.ID);
                SOIW.ShowDialog();
            }
            else
                MessageBox.Show("Выберите заказ, котоый вы хотите заказать!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            UpdateTable();
        }

        private void Change_Order_Status(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                Orders order = DGrid.SelectedItem as Orders;

                if (order.orderStatus != 1)
                {
                    MessageBox.Show("Статус заказа можно сменить только у заказов которые в пути!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                order.orderStatus = 2;
                try
                {
                    SuppliesDBEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                Thread generatingReport = new Thread(() => Generate_Report(order));
                generatingReport.Start();
            }
            else
                MessageBox.Show("Выберите заказ, который пришёл!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            UpdateTable();
        }

        void Generate_Report(Orders order)
        {
            try
            {
                List<Components> selectedComponents = new List<Components>();

                string[] textsToFind = { "~ID_Order~", "~FullName_Customer~", "~PhoneNum_Customer~", "~Customer_Addres~", "~Delivery_Date~", "~Full_Price~" };
                string[] replacements = { order.ID.ToString(), order.Clients.fullName, order.Clients.phoneNumber.ToString(), order.Clients.address, order.deliveryDate.ToString().Remove(10), order.fullPrice.ToString() };
                string[] columsName = { "Тип", "Название", "Цена" };

                DateTime date = DateTime.ParseExact(order.createDate.ToString(), "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                string formattedDate = date.ToString("ddMMyyyyHHmm");

                string checkCode = Convert.ToInt64(order.ID) + formattedDate;
                long checkCodeL = Convert.ToInt64(checkCode);

                var components = SuppliesDBEntities.GetContext().Ordered_components.Where(c => c.checkCode == checkCodeL).ToList();

                foreach (var component in components)
                {
                    selectedComponents.Add(SuppliesDBEntities.GetContext().Components.FirstOrDefault(i => i.ID == component.components_ID));
                }

                var app = new Word.Application();
                app.Visible = false;

                Word.Document doc = app.Documents.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Templates\Invoice.docx");
                Word.Document newDoc = app.Documents.Add();
                newDoc.Content.FormattedText = doc.Content.FormattedText;

                for (int i = 0; i < textsToFind.Length; i++)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = textsToFind[i];
                    find.Replacement.Text = replacements[i];
                    find.Execute(Replace: Word.WdReplace.wdReplaceAll);
                }

                Word.Find findT = app.Selection.Find;
                findT.Text = "~Order_Table~";
                findT.Execute();

                app.Selection.ClearFormatting();

                if (findT.Found)
                {
                    Word.Range range = app.Selection.Range;
                    Word.Table newTable = newDoc.Tables.Add(range, selectedComponents.Count + 1, 3);
                    newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Устанавливаем стиль линии для внутренних границ
                    newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle; // Устанавливаем стиль линии для внешних границ

                    // Заполнение заголовков таблицы
                    for (int i = 0; i < columsName.Count(); i++)
                    {
                        newTable.Cell(1, i + 1).Range.Text = columsName[i];
                    }

                    for (int row = 2; row <= selectedComponents.Count + 1; row++)
                    {
                        newTable.Cell(row, 1).Range.Text = selectedComponents[row - 2].Components_type.name; // Заполнение столбца "Тип"
                        newTable.Cell(row, 2).Range.Text = selectedComponents[row - 2].name; // Заполнение столбца "Название"
                        newTable.Cell(row, 3).Range.Text = selectedComponents[row - 2].price.ToString(); // Заполнение столбца "ценна"
                    }
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                newDoc.SaveAs(path + $@"\{order.Clients.fullName}.docx");

                newDoc.Close();
                doc.Close();

                app.Quit();

                MessageBox.Show("Статус заказа сменён и созданна накладная на рабочем столе", "Успех", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Test_Method(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem != null)
            {
                Orders order = DGrid.SelectedItem as Orders;
                if (order.orderStatus != 2)
                {
                    MessageBox.Show("Статус заказа можно сменить только у заказов которые в пути к клиенту!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                order.orderStatus = 3;

                try
                {
                    SuppliesDBEntities.GetContext().SaveChanges();

                    MessageBox.Show("Статус заказа сменён", "Успех", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Выберите заказ, который доставлен до клиента!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            UpdateTable();
        }
    }
}
