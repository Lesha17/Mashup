using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace Mashup
{
    // Конвертер используется для преобразования поля Authorized объекта класса Client 
    // к Visibility
    // Используется в MainWindow
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToButtonVisibilityConverter : IValueConverter
    {
        public const string Invert = "Invert";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility.");

            bool? bValue = (bool?)value;

            return bValue.HasValue && !bValue.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
