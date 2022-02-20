using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static readonly DependencyProperty PopupItemsProperty = DependencyProperty.Register("PopupItems", typeof(ObservableCollection<Button>), typeof(FunctionButton), new FrameworkPropertyMetadata(new ObservableCollection<Button>()));
        public ObservableCollection<Button> PopupItems
        {
            get => GetValue(PopupItemsProperty) as ObservableCollection<Button>;
            set => SetValue(PopupItemsProperty, value);
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FunctionButton)d).ComboBoxSelectedItem = ((FunctionButton)d).FunctionElements[((FunctionButton)d).ComboBoxSelectedIndex];
            ((FunctionButton)d).FunctionText = ((FunctionButton)d).FunctionElementTexts[((FunctionButton)d).ComboBoxSelectedIndex];
        }
        public static readonly DependencyProperty ComboBoxSelectedIndexProperty = DependencyProperty.Register("ComboBoxSelectedIndex", typeof(int), typeof(FunctionButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnSelectedIndexChanged)));
        public int ComboBoxSelectedIndex
        {
            get => (int)GetValue(ComboBoxSelectedIndexProperty);
            set => SetValue(ComboBoxSelectedIndexProperty, value);
        }

        public static readonly DependencyProperty ComboBoxSelectedItemProperty = DependencyProperty.Register("ComboBoxSelectedItem", typeof(FrameworkElement), typeof(FunctionButton));
        public FrameworkElement ComboBoxSelectedItem
        {
            get => GetValue(ComboBoxSelectedItemProperty) as FrameworkElement;
            set => SetValue(ComboBoxSelectedItemProperty, value);
        }

        public static readonly DependencyProperty IsComboBoxDropDownOpenProperty = DependencyProperty.Register("IsComboBoxDropDownOpen", typeof(bool), typeof(FunctionButton));
        public bool IsComboBoxDropDownOpen
        {
            get => (bool)GetValue(IsComboBoxDropDownOpenProperty);
            set => SetValue(IsComboBoxDropDownOpenProperty, value);
        }

        public FunctionButton()
        {
            InitializeComponent();
            SetValue(PopupItemsProperty, new ObservableCollection<Button>());
            SetValue(FunctionElementsProperty, new List<FrameworkElement>());
            SetValue(FunctionElementTextsProperty, new List<string>());
        }

        private void PART_Popup_Drop(object sender, DragEventArgs e)
        {
            (sender as Popup).HorizontalOffset = -10;
        }
    }
}
