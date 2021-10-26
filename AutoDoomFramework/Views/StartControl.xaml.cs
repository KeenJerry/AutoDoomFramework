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
    /// Interaction logic for StartControl.xaml
    /// </summary>
    public partial class StartControl : UserControl
    {
        public StartControl()
        {
            InitializeComponent();
        }

        private void CreateProcessButton_Click(object sender, RoutedEventArgs e)
        {
            CreateProcessWindow createProcessWindow = new CreateProcessWindow();
            Window parent = Window.GetWindow(this);
            createProcessWindow.Owner = parent;
            createProcessWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createProcessWindow.ShowDialog();
        }

        private void OpenRecentProjectButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Owner = Window.GetWindow(this);
            loadingWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            loadingWindow.Owner.Hide();
            loadingWindow.Show();
        }
    }
}
