using AutoDoomFramework.Models.ToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Services.Interfaces
{
    interface IActivityService
    {
        void LoadActivities(string assemblyName);

        void LoadActivities(List<string> assemblyNames);

        List<Assembly> GetLoadedAssemblies();

        Assembly FindDefaultActivityAssembly();

        DActivityCategory LoadDefaultActivities();

        DActivityCategory LoadOCRActivities();

        List<DActivityCategory> GetAllActivities();
    }
}
