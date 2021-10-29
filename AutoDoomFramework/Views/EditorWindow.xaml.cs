using AutoDoomFramework.ViewModels;
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
using System.Windows.Shapes;

namespace AutoDoomFramework.Views
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();
        }

        private void TopBarCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TopBarScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                BorderThickness = new Thickness(7);
            }
            else
            {
                WindowState = WindowState.Normal;
                BorderThickness = new Thickness(0);
            }
        }

        private void TopBarMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void EditorDockingManager_DocumentClosed(object sender, AvalonDock.DocumentClosedEventArgs e)
        {
            e.Document.IsSelected = true;
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.CloseWorkflowCommand.Execute((e.Document.Content as Border).Tag as string);
        }

        private void SaveFunctionButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
