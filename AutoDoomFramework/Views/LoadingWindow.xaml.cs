using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        public void OnEditorLoaded()
        {
            EditorWindow editorWindow = new EditorWindow();
            this.Owner.Close();

            editorWindow.Show();
        }

        public void OnInitialProcessFailed()
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
