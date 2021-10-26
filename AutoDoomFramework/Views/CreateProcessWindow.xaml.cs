using AutoDoomFramework.Models;
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

using Ookii.Dialogs.Wpf;
using AutoDoomFramework.ViewModels;
using Prism.Events;
using System.Threading;

namespace AutoDoomFramework.Views
{
    /// <summary>
    /// Interaction logic for CreateProcessWindow.xaml
    /// </summary>
    public partial class CreateProcessWindow : Window
    {
        public CreateProcessWindow()
        {
            InitializeComponent();
        }

        private void TopBarCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TopBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CreateProcessWindowViewModel viewModel = (CreateProcessWindowViewModel)DataContext;
            viewModel.NameTextChangeCommand.Execute(NameTextBox.Text);
        }

        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CreateProcessWindowViewModel viewModel = (CreateProcessWindowViewModel)DataContext;
            viewModel.LocationTextChangeCommand.Execute(LocationTextBox.Text);
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CreateProcessWindowViewModel viewModel = (CreateProcessWindowViewModel)DataContext;
            viewModel.DescriptionTextChangeCommand.Execute(DescriptionTextBox.Text);
        }

        private void SelectLocationButton_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                LocationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void OpenEditorButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Owner = this.Owner;
            loadingWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Owner.Hide();
            this.Close();
            loadingWindow.Show();
        }
    }
}
