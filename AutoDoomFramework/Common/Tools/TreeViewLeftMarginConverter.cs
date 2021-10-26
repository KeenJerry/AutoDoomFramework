using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AutoDoomFramework.Common.Tools
{
    class TreeViewLeftMarginConverter : IValueConverter
    {
        public double MarginLength { get; set; }

        private TreeViewItem GetParent(TreeViewItem item)
        {
            if (item is null) return null;

            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while(!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }

        private int GetDepth(TreeViewItem item)
        {
            int depth = 0;
            TreeViewItem parent = GetParent(item);
            while(parent != null)
            {
                depth++;
                parent = GetParent(parent);
            }

            return depth;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewItem)
            {
                TreeViewItem item = value as TreeViewItem;
                if (item == null)
                {
                    return new Thickness(0);
                } else
                {
                    return new Thickness(MarginLength * GetDepth(item), 0, 0, 0);
                }
            }

            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
