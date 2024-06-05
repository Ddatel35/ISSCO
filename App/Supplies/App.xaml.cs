using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Supplies
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    public class IntToStringConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue == 0)
                    return "Не заказан";
                if (intValue == 1)
                    return "В пути";
                if (intValue == 2)
                    return "Доставка до клиента";
                if (intValue == 3)
                    return "У клиента";
                if (intValue == 4)
                    return "Отмененно";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
