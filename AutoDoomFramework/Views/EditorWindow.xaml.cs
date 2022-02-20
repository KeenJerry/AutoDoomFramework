using AutoDoomFramework.ViewModels;
using AutoDoomFramework.Views.Icons;
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
            // TODO
            if (WindowState == WindowState.Normal)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
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

        private void Switch2Save_Click(object sender, RoutedEventArgs e)
        {
            SaveWorkflowButton.IsComboBoxDropDownOpen = false;
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.SelectSaveButtonIndexCommand.Execute(0);
        }

        private void Switch2SaveAll_Click(object sender, RoutedEventArgs e)
        {
            SaveWorkflowButton.IsComboBoxDropDownOpen = false;
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.SelectSaveButtonIndexCommand.Execute(1);
        }

        private void RunWorkflow(object sender, MouseButtonEventArgs e)
        {
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.RunWorkflowCommand.Execute();
        }

        private void Switch2Run_Click(object sender, RoutedEventArgs e)
        {
            ExecuteWorkflowButton.IsComboBoxDropDownOpen = false;
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.SelectExecuteButtonIndexCommand.Execute(0);
        }

        private void Switch2Debug_Click(object sender, RoutedEventArgs e)
        {
            ExecuteWorkflowButton.IsComboBoxDropDownOpen = false;
            EditorWindowViewModel viewModel = (EditorWindowViewModel)DataContext;
            viewModel.SelectExecuteButtonIndexCommand.Execute(1);
        }
    }
}
