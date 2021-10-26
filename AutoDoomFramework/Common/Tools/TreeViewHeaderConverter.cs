using AutoDoomFramework.Models.ToolBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutoDoomFramework.Common.Tools
{
    class TreeViewHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DActivity)
            {
                return (value as DActivity).DisplayName;
            }

            if (value is DActivityCategory)
            {
                return (value as DActivityCategory).CategoryName;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
