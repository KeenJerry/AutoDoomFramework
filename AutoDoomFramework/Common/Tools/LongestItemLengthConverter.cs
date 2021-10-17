using AutoDoomFramework.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AutoDoomFramework.Common.Tools
{
    public class LongestItemLengthConverter : IValueConverter
    {
        public LongestItemLengthConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is List<ComboBoxItem>)
            {
                return (value as List<ComboBoxItem>).Max(item => item.ActualWidth);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
