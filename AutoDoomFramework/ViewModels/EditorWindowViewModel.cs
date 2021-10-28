using AutoDoomFramework.Common.Tools;
using AutoDoomFramework.Models;
using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Services.Interfaces;
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

        private ObservableCollection<Border> workflowDesigners = new ObservableCollection<Border>();
        public ObservableCollection<Border> WorkflowDesigners
        {
            get => workflowDesigners;
            private set { }
        }

        public DelegateCommand<string> OpenWorkflowCommand { get; private set; }
        private void OpenWorkflow(string workflowName)
        {
            if (WorkflowDesigners.Count != 0)
            {
                if (!(WorkflowDesigners.First(designer => (designer.Tag as string) == workflowName) is null))
                {
                    return;
                }
            }
            
            new DesignerMetadata().Register();
            WorkflowDesigner wf = new WorkflowDesigner
            {
                Text = workflowName
            };

            WorkflowDesignerIcons.UseWindowsStoreAppStyleIcons();
            wf.PropertyInspectorFontAndColorData = resourceService.GetWorkflowDesignerResource();

            Sequence sequence = new Sequence
            {
                DisplayName = SuffixCutter.Cut(workflowName, ".xaml")
            };
            wf.Load(Path.Combine(cacheService.GetWorkingRegistry().Location, cacheService.GetWorkingRegistry().Name, workflowName));

            Border border = new Border();
            border.Child = wf.View;

            border.Tag = workflowName;

            WorkflowDesigners.Add(border);

        }

        public EditorWindowViewModel(ICacheService cacheService, IActivityService activityService, IResourceService resourceService)
        {
            this.cacheService = cacheService;
            this.resourceService = resourceService;
            this.activityService = activityService;

            RegistryName = cacheService.GetWorkingRegistryName();

            activityService.LoadDefaultActivities();

            OpenWorkflowCommand = new DelegateCommand<string>(OpenWorkflow);
        }
    }
}
