using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoDoomFramework.Services.Interfaces
{
    interface IResourceService
    {
        void LoadWorkflowDesignerResource();

        string GetWorkflowDesignerResource();
    }
}
