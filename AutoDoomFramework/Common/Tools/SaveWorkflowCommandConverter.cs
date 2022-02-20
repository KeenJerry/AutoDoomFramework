using AutoDoomFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutoDoomFramework.Common.Tools
{
    class SaveWorkflowCommandConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch (values[0])
            {
                case 0:
                    {
                        return (values[1] as EditorWindowViewModel).SaveWorkflowCommand;
                    }
                case 1:
                    {
                        return (values[1] as EditorWindowViewModel).SaveAllWorkflowCommand;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
