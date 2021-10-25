using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoDoomFramework.Models.ToolBox
{
    class DActivity: Activity
    {
        private ImageSource imageSource;
        public ImageSource Icon 
        {
            get => imageSource;
            set => imageSource = value;
        }

        private Type typeName;
        public Type TypeName
        {
            get => typeName;
            set => typeName = value;
        }

        private string assemblyName;
        public string AssemblyName
        {
            get => assemblyName;
            set => assemblyName = value;
        }

        public DActivity(Type type, ImageSource icon, string assemblyName)
        {
            TypeName = type;
            Icon = icon;
            AssemblyName = assemblyName;
        }
    }
}
