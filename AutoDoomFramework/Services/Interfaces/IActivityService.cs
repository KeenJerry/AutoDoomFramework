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

        string[] GetAppDefaultActivityNames();

        List<Assembly> GetLoadedAssemblies();

        Assembly FindSystemActivityAssembly();

        DActivityCategory LoadSystemActivities();

        DActivityCategory LoadOCRActivities();

        DActivityCategory LoadRenderActivities();

        DActivityCategory LoadElementActivities();

        DActivityCategory LoadDialDetectionActivities();

        DActivityCategory LoadRTMPActivities();

        List<DActivityCategory> GetAllActivities();
    }
}
