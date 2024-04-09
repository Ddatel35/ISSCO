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
using Excel = Microsoft.Office.Interop.Excel;

namespace Приложение_Курсача.WindowsAndFrames
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        LaborExchangeEntities db = new LaborExchangeEntities();

        /* Инициализация всех компонентов и вывод закрытых вакансий из БД */
        public ReportPage()
        {
            InitializeComponent();
            var list = from vac in db.Vacancies where vac.Status == false select vac;
            DGrid.ItemsSource = list.ToList();
            CountVac.Text = Convert.ToString(list.Count());
        }


        /* Обработчик кнопки для экспорта закрытых вакансий в Excel */
        private void ReportPageEx_Btn(object sender, RoutedEventArgs e)
        {
            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;

            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            worksheet.Name = "Закрытые вакансии";

            worksheet.Cells[1][startRowIndex] = "Название вакансии";
            worksheet.Cells[2][startRowIndex] = "Зарплата";
            worksheet.Cells[3][startRowIndex] = "Требуемое образование";
            worksheet.Cells[4][startRowIndex] = "Рабочий график";
            worksheet.Cells[5][startRowIndex] = "Адрес";

            startRowIndex++;

            var closeVacansies = db.Vacancies.Where(p => p.Status == false);

            foreach (var vacansies in closeVacansies)
            {
                worksheet.Cells[1][startRowIndex] = vacansies.Name;
                worksheet.Cells[2][startRowIndex] = vacansies.Sallary;
                worksheet.Cells[3][startRowIndex] = vacansies.RequeredEdications;
                worksheet.Cells[4][startRowIndex] = vacansies.Schedule;
                worksheet.Cells[5][startRowIndex] = vacansies.Address;

                startRowIndex++;
            }

            Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
            sumRange.Merge();
            sumRange.Value = "Всего закрытых вакансий:";
            sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            worksheet.Cells[5][startRowIndex] = closeVacansies.Count();

            Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[5][startRowIndex]];
            rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
            rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
            rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
            rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
            rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
            rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

            worksheet.Columns.AutoFit();

            application.Visible = true;
        }
    }
}
