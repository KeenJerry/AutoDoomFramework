using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AutoDoomFramework.Views
{
    /// <summary>
    /// Interaction logic for FunctionButton.xaml
    /// </summary>
    public partial class FunctionButton : UserControl
    {
        public static readonly DependencyProperty SelectedCommandProperty = DependencyProperty.Register("SelectedCommand", typeof(ICommand), typeof(FunctionButton));
        public ICommand SelectedCommand
        {
            get => GetValue(SelectedCommandProperty) as ICommand;
            set => SetValue(SelectedCommandProperty, value);
        }

        public static readonly DependencyProperty IsComboBoxEnabledProperty = DependencyProperty.Register("IsComboBoxEnabled", typeof(bool), typeof(FunctionButton), new FrameworkPropertyMetadata(true));
        public bool IsComboBoxEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty ButtonImageSourceProperty = DependencyProperty.Register("ButtonImageSource", typeof(ImageSource), typeof(FunctionButton));
        public ImageSource ButtonImageSource
        {
            get => GetValue(ButtonImageSourceProperty) as ImageSource;
            set => SetValue(ButtonImageSourceProperty, value);
        }

        public static readonly DependencyProperty FunctionElementsProperty = DependencyProperty.Register("FunctionElements", typeof(List<FrameworkElement>), typeof(FunctionButton), new FrameworkPropertyMetadata(new List<FrameworkElement>()));
        public List<FrameworkElement> FunctionElements
        {
            get => GetValue(FunctionElementsProperty) as List<FrameworkElement>;
            set => SetValue(FunctionElementsProperty, value);
        }

        public static readonly DependencyProperty FunctionElementTextsProperty = DependencyProperty.Register("FunctionElementTexts", typeof(List<string>), typeof(FunctionButton), new FrameworkPropertyMetadata(new List<string>()));
        public List<string> FunctionElementTexts
        {
            get => GetValue(FunctionElementTextsProperty) as List<string>;
            set => SetValue(FunctionElementTextsProperty, value);
        }

        public static readonly DependencyProperty FunctionTextProperty = DependencyProperty.Register("FunctionText", typeof(string), typeof(FunctionButton));
        public string FunctionText
        {
            get => GetValue(FunctionTextProperty) as string;
            set => SetValue(FunctionTextProperty, value);
        }

        public static readonly DependencyProperty PopupItemsProperty = DependencyProperty.Register("PopupItems", typeof(List<Button>), typeof(FunctionButton), new FrameworkPropertyMetadata(new List<Button>()));
        public List<Button> PopupItems
        {
            get => GetValue(PopupItemsProperty) as List<Button>;
            set => SetValue(PopupItemsProperty, value);
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FunctionButton)d).SelectedItem = ((FunctionButton)d).FunctionElements[((FunctionButton)d).SelectedIndex];
            ((FunctionButton)d).FunctionText = ((FunctionButton)d).FunctionElementTexts[((FunctionButton)d).SelectedIndex];
        }
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(FunctionButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnSelectedIndexChanged)));
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(FrameworkElement), typeof(FunctionButton));
        public FrameworkElement SelectedItem
        {
            get => GetValue(SelectedItemProperty) as FrameworkElement;
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(FunctionButton));
        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        public FunctionButton()
        {
            InitializeComponent();
            SetValue(PopupItemsProperty, new List<Button>());
            SetValue(FunctionElementsProperty, new List<FrameworkElement>());
            SetValue(FunctionElementTextsProperty, new List<string>());
        }

        private void PART_Popup_Drop(object sender, DragEventArgs e)
        {
            (sender as Popup).HorizontalOffset = -10;
        }
    }
}
