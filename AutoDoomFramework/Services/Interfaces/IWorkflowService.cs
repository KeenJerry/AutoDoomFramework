using AutoDoomFramework.ViewModels;
using System;
using System.Activities.Presentation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Services.Interfaces
{
    interface IWorkflowService
    {
        void Run(WorkflowDesigner wfDesigner, EditorWindowViewModel editorWindowViewModel);

        void Terminate();
    }
}
