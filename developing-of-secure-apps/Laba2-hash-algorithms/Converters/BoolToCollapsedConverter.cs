using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Laba2_hash_algorithms.Converters
{
    public enum ConverterParam
    {
        Normal,
        Inverted
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var inverted = IsInverted(parameter);

            var val = inverted ? !(bool)value : (bool)value;

            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var inverted = IsInverted(parameter);

            var visibilityValue = (Visibility)value;

            switch (visibilityValue)
            {
                case Visibility.Visible:
                    return inverted ? false : true;
                default:
                    return inverted ? true : false;
            }
        }

        private bool IsInverted(object parameter)
        {
            var paramString = parameter == null ? string.Empty : parameter.ToString();
            ConverterParam param;
            if (!Enum.TryParse<ConverterParam>(paramString, out param))
            {
                param = ConverterParam.Normal;
            }

            return ConverterParam.Inverted == param;
        }
    }
}
