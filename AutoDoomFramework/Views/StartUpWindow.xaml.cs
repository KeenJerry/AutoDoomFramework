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

using AutoDoomFramework.ViewModels;

namespace AutoDoomFramework.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
        }

        private void TopBarCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Restore\Maximize window size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // TODO 窗口最大化后会遮住任务栏
        private void TopBarScaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                BorderThickness = new Thickness(7);
            } else
            {
                WindowState = WindowState.Normal;
                BorderThickness = new Thickness(0);
            }
        }
        
        /// <summary>
        /// Minimize window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopBarMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        /// <summary>
        /// Enable window dragging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void OpenProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                DefaultExt = ".adm",
                Filter = "AutoDoom File|.adm"
            };
            dialog.ShowDialog();
        }
    }
}
