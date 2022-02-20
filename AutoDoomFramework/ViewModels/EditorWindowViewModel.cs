using AutoDoomFramework.Common.Events;
using AutoDoomFramework.Common.Tools;
using AutoDoomFramework.Models;
using AutoDoomFramework.Models.Project;
using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Properties;
using AutoDoomFramework.Services.Interfaces;
using AutoDoomFramework.Views.Icons;
using AvalonDock.Layout;
using Prism.Commands;
using Prism.Events;
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

        private readonly IEventAggregator eventAggregator;

        #region ExecuteButton
        // ExecuteButton Binding Parameters
        private int OriginExecuteButtonSelectedIndex;

        private int executeButtonSelectedIndex = 0;
        public int ExecuteButtonSelectedIndex
        {
            get => executeButtonSelectedIndex;
            set
            {
                SetProperty(ref executeButtonSelectedIndex, value);
                RaisePropertyChanged(nameof(ExecuteButtonSelectedIndex));
            }
        }

        private bool executeButtonComboBoxEnabled = true;
        public bool ExecuteButtonComboBoxEnabled
        {
            get => executeButtonComboBoxEnabled;
            set
            {
                SetProperty(ref executeButtonComboBoxEnabled, value);
                RaisePropertyChanged(nameof(ExecuteButtonComboBoxEnabled));
            }
        }

        public void PublishProcessStartEvent()
        {
            eventAggregator.GetEvent<ProcessStartedEvent>().Publish();
        }
        private void OnProcessStart()
        {
            Console.WriteLine("EditorWindowViewModel Process Started");
            OriginExecuteButtonSelectedIndex = ExecuteButtonSelectedIndex;
            ExecuteButtonSelectedIndex = 2;
            ExecuteButtonComboBoxEnabled = false;
        }

        public void PublishProcessCompleteEvent()
        {
            eventAggregator.GetEvent<ProcessCompleteEvent>().Publish();
        }
        private void OnProcessComplete()
        {
            Console.WriteLine("EditorWindowViewModel Process Completed");
            ExecuteButtonSelectedIndex = OriginExecuteButtonSelectedIndex;
            ExecuteButtonComboBoxEnabled = true;
        }

        public void PublishProcessTerminateEvent()
        {
            eventAggregator.GetEvent<ProcessTerminateEvent>().Publish();
        }
        private void OnProcessTerminate()
        {
            Console.WriteLine("EditorWindowViewModel Process Terminated");
            ExecuteButtonSelectedIndex = OriginExecuteButtonSelectedIndex;
            ExecuteButtonComboBoxEnabled = true;
        }

        private void SelectExecuteButtonIndex(object index)
        {
            if (!(index is null))
            {
                if (index is int)
                {
                    ExecuteButtonSelectedIndex = (int)index;
                }
            }
        }
        public DelegateCommand<object> SelectExecuteButtonIndexCommand { get; private set; }

        // ExecuteButton Command
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
                wf.Save(Path.Combine(WorkingRegistry.Location, WorkingRegistry.Name, mainWorkflowName));

                workflowService.Run(wf, this);
            }
        }

        public DelegateCommand DebugWorkflowCommand { get; private set; }
        private void DebugWorkflow()
        {
            Console.WriteLine("Debug Start");
        }

        public DelegateCommand TerminateWorkflowCommand { get; private set; }
        private void TerminateWorkflow()
        {
            workflowService.Terminate();
        }
        #endregion

        #region SaveButton
        private int saveButtonSelectedIndex = 0;
        public int SaveButtonSelectedIndex
        {
            get => saveButtonSelectedIndex;
            set
            {
                SetProperty(ref saveButtonSelectedIndex, value);
                RaisePropertyChanged(nameof(SaveButtonSelectedIndex));
            }
        }

        private void SelectedSaveButtonIndex(object index) 
        {
            if (!(index is null))
            {
                if (index is int @id)
                {
                    SaveButtonSelectedIndex = @id;
                }
            }
        }
        public DelegateCommand<object> SelectSaveButtonIndexCommand { get; private set; }

        // SaveButton Command
        public DelegateCommand SaveWorkflowCommand { get; private set; }
        private void SaveWorkflow()
        {
            Console.WriteLine("Save Workflow");
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
                wf.Save(Path.Combine(WorkingRegistry.Location, WorkingRegistry.Name, mainWorkflowName));
            }
        }

        public DelegateCommand SaveAllWorkflowCommand { get; private set; }
        private void SaveAllWorkflow() { }
        #endregion


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

        public EditorWindowViewModel(ICacheService cacheService, IActivityService activityService, IResourceService resourceService, IWorkflowService workflowService, IEventAggregator eventAggregator)
        {
            #region Service Initialize
            this.cacheService = cacheService;
            this.resourceService = resourceService;
            this.activityService = activityService;
            this.workflowService = workflowService;
            #endregion

            #region Event Aggregator Initialize
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ProcessStartedEvent>().Subscribe(OnProcessStart);
            eventAggregator.GetEvent<ProcessCompleteEvent>().Subscribe(OnProcessComplete);
            eventAggregator.GetEvent<ProcessTerminateEvent>().Subscribe(OnProcessTerminate);
            #endregion

            RegistryName = cacheService.GetWorkingRegistryName();

            #region LoadActivities
            activityService.LoadSystemActivities();
            activityService.LoadOCRActivities();
            activityService.LoadRenderActivities();
            activityService.LoadElementActivities();
            activityService.LoadDialDetectionActivities();
            //activityService.LoadRTMPActivities();
            #endregion

            #region CommandInitialize
            #region OpenAndCloseCommand
            OpenWorkflowCommand = new DelegateCommand<string>(OpenWorkflow);
            CloseWorkflowCommand = new DelegateCommand<string>(CloseWorkflow);
            #endregion
            #region ExecuteCommand
            RunWorkflowCommand = new DelegateCommand(RunWorkflow);
            DebugWorkflowCommand = new DelegateCommand(DebugWorkflow);
            TerminateWorkflowCommand = new DelegateCommand(TerminateWorkflow);
            SelectExecuteButtonIndexCommand = new DelegateCommand<object>(SelectExecuteButtonIndex);
            #endregion
            #region SaveCommand
            SaveWorkflowCommand = new DelegateCommand(SaveWorkflow);
            SaveAllWorkflowCommand = new DelegateCommand(SaveAllWorkflow);
            SelectSaveButtonIndexCommand = new DelegateCommand<object>(SelectedSaveButtonIndex);
            #endregion
            #endregion
        }
    }
}
