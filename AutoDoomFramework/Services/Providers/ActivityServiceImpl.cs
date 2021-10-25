using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Services.Interfaces;
using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoDoomFramework.Services.Providers
{
    class ActivityServiceImpl : IActivityService
    {
        private List<DActivityCategory> toolsTree = new List<DActivityCategory>();
        public List<DActivityCategory> ToolsTree
        {
            get => toolsTree;
            private set { }
        }

        public List<DActivityCategory> GetAllActivities()
        {
            return ToolsTree;
        }

        public DActivityCategory LoadDefaultActivities()
        {
            DActivityCategory dActivityCategory = new DActivityCategory("System");
            DActivityCategory controlCategory = new DActivityCategory("Control");

            Assembly defaultAssembly = FindDefaultActivityAssembly();
            if (defaultAssembly is null)
            {
                return dActivityCategory; // TODO Warn user of the none existence.
            }

            foreach(Type t in defaultAssembly.ExportedTypes)
            {
                if (typeof(Activity).IsAssignableFrom(t))
                {
                    if (t.Namespace == "System.Activities.Statements")
                    {
                        switch (t.Name)
                        {
                            case "If":
                                {
                                    controlCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/if.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "While":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/while.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "StateMachine":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/statemachine.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "Sequence":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/sequence.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "Assign":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/assign.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "Delay":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/delay.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "DoWhile":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/dowhile.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            case "WriteLine":
                                {
                                    dActivityCategory.AddActivity(new DActivity(t, new BitmapImage(new Uri(@"/Assets/Images/writeline.png", UriKind.Relative)), "System.Activity"));
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }
                        
                    }
                }
            }

            dActivityCategory.AddCategory(controlCategory);
            toolsTree.Add(dActivityCategory);
            return dActivityCategory;
        }

        public void LoadActivities(string assemblyName)
        {
            
        }

        public void LoadActivities(List<string> assemblyNames)
        {
            throw new NotImplementedException();
        }

        public List<Assembly> GetLoadedAssemblies()
        {
             return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }

        public Assembly FindDefaultActivityAssembly()
        {
            foreach(Assembly assembly in GetLoadedAssemblies())
            {
                if (assembly.GetName().ToString() == "System.Activities, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")
                {
                    return assembly;
                }
            }

            return null;
        }
    }
}
