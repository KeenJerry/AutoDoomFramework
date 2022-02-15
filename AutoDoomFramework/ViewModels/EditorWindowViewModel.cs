﻿using AutoDoomFramework.Common.Tools;
using AutoDoomFramework.Models;
using AutoDoomFramework.Models.Project;
using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Properties;
using AutoDoomFramework.Services.Interfaces;
using AutoDoomFramework.Views.Icons;
using AvalonDock.Layout;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xaml;
using System.Xml;

namespace AutoDoomFramework.ViewModels
{
    class EditorWindowViewModel: BindableBase
    {
        private readonly ICacheService cacheService;
        private readonly IResourceService resourceService;
        private readonly IActivityService activityService;
        private readonly IWorkflowService workflowService;

        private string registryName;
        public string RegistryName
        {
            get => registryName;
            set
            {
                SetProperty(ref registryName, value);
                RaisePropertyChanged(nameof(RegistryName));
            }
        }

        private Button executeIcon = null;
        public Button ExecuteIcon
        {
            get => executeIcon;
            set
            {
                SetProperty(ref executeIcon, value);
                RaisePropertyChanged(nameof(ExecuteIcon));
            }
        }

        public ref Registry WorkingRegistry
        {
            get
            {
                return ref cacheService.GetWorkingRegistry();
            }
        }

        public List<Registry> Registries
        {
            get
            {
                return new List<Registry> { WorkingRegistry };
            }
        }

        public List<DActivityCategory> DActivityCategories
        {
            get => activityService.GetAllActivities();
            set
            {
                RaisePropertyChanged(nameof(DActivityCategories));
            }
        }

        private List<WorkflowDesigner> workflowDesigners = new List<WorkflowDesigner>();

        private ObservableCollection<Border> anchorables = new ObservableCollection<Border>();
        public ObservableCollection<Border> Anchorables
        {
            get => anchorables;
            private set { }
        }

        private ObservableCollection<Border> documents = new ObservableCollection<Border>();
        public ObservableCollection<Border> Documents
        {
            get => documents;
            private set { }
        }

        public DelegateCommand<string> OpenWorkflowCommand { get; private set; }
        private void OpenWorkflow(string workflowName)
        {
            if (Documents.Count != 0)
            {
                if (!(Documents.First(designer => (designer.Tag as string) == workflowName) is null))
                {
                    return;
                }
            }

            WorkflowDesigner wf = null;
            switch (WorkingRegistry)
            {
                case DProcess process:
                    {
                        Workflow workflow = process.WorkflowCollection.Workflows.Find(item => item.FileName == workflowName);
                        if (workflow is null)
                        {
                            return;
                        }

                        wf = workflow.Instance;
                        break;
                    }
            }

            Border viewBorder = new Border();
            viewBorder.Child = wf.View;
            viewBorder.Tag = workflowName;

            if (Anchorables.Count == 0)
            {
                Border propertyBorder = new Border();
                propertyBorder.Child = wf.PropertyInspectorView;
                propertyBorder.Tag = "Properties";
                Anchorables.Add(propertyBorder);
            }
            else
            {
                Border bd = Anchorables.First(border => (border.Tag as string) == "Properties");
                if (bd is null)
                {
                    Border propertyBorder = new Border();
                    propertyBorder.Child = wf.PropertyInspectorView;
                    propertyBorder.Tag = "Properties";
                    Anchorables.Add(propertyBorder);
                }
                else
                {
                    if (bd.Child == null)
                    {
                        bd.Child = wf.PropertyInspectorView;
                    }
                }
            }

            Documents.Add(viewBorder);
            workflowDesigners.Add(wf);
        }

        public DelegateCommand<string> CloseWorkflowCommand { get; private set; }
        private void CloseWorkflow(string workflowName)
        {
            Border viewBD = documents.First(border => (border.Tag as string) == workflowName);
            Documents.Remove(viewBD);

            Border propertyBD = anchorables.FirstOrDefault(border => (border.Tag as string) == "Properties");
            propertyBD.Child = null;

            WorkflowDesigner wd = workflowDesigners.First(workflowDesigner => workflowDesigner.Text == workflowName);
            wd.Save(Path.Combine(cacheService.GetWorkingRegistry().Location, cacheService.GetWorkingRegistry().Name, workflowName));
        }

        public DelegateCommand RunWorkflowCommand { get; private set; }
        private void RunWorkflow()
        {
            string mainWorkflowName = (WorkingRegistry as DProcess).MainWorkflow.FileName;

            WorkflowDesigner wf = null;
            switch (WorkingRegistry)
            {
                case DProcess process:
                    {
                        wf = (WorkingRegistry as DProcess).WorkflowCollection.MainWorkflow.Instance;
                        break;
                    }
            }

            if (wf is null)
            {
                Console.WriteLine("Workflow not found.");
            }
            else
            {

                wf.Flush();
                wf.Save(Path.Combine(WorkingRegistry.Location, WorkingRegistry.Name,mainWorkflowName));

                workflowService.Run(wf);
                //MemoryStream workflowStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(wf.Text));

                //ActivityXamlServicesSettings settings = new ActivityXamlServicesSettings()
                //{
                //    CompileExpressions = true
                //};

                //Activity activityExecute = ActivityXamlServices.Load(workflowStream, settings);

                //WorkflowApplication workflowApplication = new WorkflowApplication(activityExecute);
                //workflowApplication.Run();
            }
        }

        public DelegateCommand DebugWorkflowCommand { get; private set; }
        private void DebugWorkflow() 
        {
            Console.WriteLine("Debug Start");
        }

        public DelegateCommand StopWorkflowCommand { get; private set; }
        private void StopWorkflow() { }

        public DelegateCommand SaveWorkflowCommand { get; private set; }
        private void SaveWorkflow() { }

        public DelegateCommand SaveAllWorkflowCommand { get; private set; }
        private void SaveAllWorkflow() { }

        public DelegateCommand SaveAsWorkflowCommand { get; private set; }
        private void SaveAsWorkflow() { }


        public EditorWindowViewModel(ICacheService cacheService, IActivityService activityService, IResourceService resourceService, IWorkflowService workflowService)
        {
            this.cacheService = cacheService;
            this.resourceService = resourceService;
            this.activityService = activityService;
            this.workflowService = workflowService;

            RegistryName = cacheService.GetWorkingRegistryName();

            activityService.LoadSystemActivities();
            activityService.LoadOCRActivities();
            activityService.LoadRenderActivities();
            activityService.LoadElementActivities();

            activityService.LoadDialDetectionActivities();
            //activityService.LoadRTMPActivities();

            OpenWorkflowCommand = new DelegateCommand<string>(OpenWorkflow);
            CloseWorkflowCommand = new DelegateCommand<string>(CloseWorkflow);
            RunWorkflowCommand = new DelegateCommand(RunWorkflow);
            DebugWorkflowCommand = new DelegateCommand(DebugWorkflow);
            StopWorkflowCommand = new DelegateCommand(StopWorkflow);
            SaveWorkflowCommand = new DelegateCommand(SaveWorkflow);
            SaveAllWorkflowCommand = new DelegateCommand(SaveAllWorkflow);
            SaveAsWorkflowCommand = new DelegateCommand(SaveAsWorkflow);
        }
    }
}
