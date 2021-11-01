using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ToolBoxControl.xaml
    /// </summary>
    public partial class ToolBoxControl : UserControl
    {
        public ToolBoxControl()
        {
            InitializeComponent();
        }

        private void Expander_Click(object sender, RoutedEventArgs e)
        {
            (sender as ToggleButton).IsChecked = true;
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Type t = (sender as StackPanel).Tag as Type;
                DataObject data = new DataObject(System.Activities.Presentation.DragDropHelper.WorkflowItemTypeNameFormat, t.AssemblyQualifiedName);

                DragDrop.DoDragDrop(this, data, DragDropEffects.All);
            }
        }
    }
}
