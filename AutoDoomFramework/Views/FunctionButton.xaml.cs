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

namespace AutoDoomFramework.Views
{
    /// <summary>
    /// Interaction logic for FunctionButton.xaml
    /// </summary>
    public partial class FunctionButton : UserControl
    {

        public static readonly DependencyProperty ButtonImageSourceProperty = DependencyProperty.Register("ButtonImageSource", typeof(ImageSource), typeof(FunctionButton));
        public ImageSource ButtonImageSource
        {
            get => GetValue(ButtonImageSourceProperty) as ImageSource;
            set => SetValue(ButtonImageSourceProperty, value);
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register("PopupContent", typeof(string), typeof(FunctionButton));
        public string PopupContent
        {
            get => GetValue(PopupContentProperty) as string;
            set => SetValue(PopupContentProperty, value);
        }

        public static readonly DependencyProperty PopupItemsProperty = DependencyProperty.Register("PopupItems", typeof(List<Button>), typeof(FunctionButton), new PropertyMetadata(new List<Button>()));
        public List<Button> PopupItems
        {
            get => GetValue(PopupItemsProperty) as List<Button>;
            set => SetValue(PopupItemsProperty, value);
        }

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(FrameworkElement), typeof(FunctionButton));
        public FrameworkElement ButtonContent
        {
            get => GetValue(ButtonContentProperty) as FrameworkElement;
            set => SetValue(ButtonContentProperty, value);
        }

        public FunctionButton()
        {
            SetValue(PopupItemsProperty, new List<Button>());
            InitializeComponent();
        }
    }
}
