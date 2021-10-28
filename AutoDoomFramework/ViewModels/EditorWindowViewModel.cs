using AutoDoomFramework.Models;
using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Services.Interfaces;
using AvalonDock.Layout;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Activities.Presentation;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoDoomFramework.ViewModels
{
    class EditorWindowViewModel: BindableBase
    {
        private readonly ICacheService cacheService;
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

        private List<Border> workflowDesigners = new List<Border>();
        public List<Border> WorkflowDesigners
        {
            get => workflowDesigners;
            set
            {
                SetProperty(ref workflowDesigners, value);
                RaisePropertyChanged(nameof(WorkflowDesigners));
            }
        }

        public DelegateCommand OpenMainXamlCommand { get; private set; }
        private void OpenMainXaml() {
            Console.WriteLine("DDD");
        }

        public EditorWindowViewModel(ICacheService cacheService, IActivityService activityService)
        {
            this.cacheService = cacheService;
            this.activityService = activityService;

            RegistryName = cacheService.GetWorkingRegistryName();

            activityService.LoadDefaultActivities();

            OpenMainXamlCommand = new DelegateCommand(OpenMainXaml);

            WorkflowDesigner wf = new WorkflowDesigner();
            wf.Text = "Main";
            wf.Load(new Sequence());

            Border border = new Border();
            border.Child = wf.View;

            border.Tag = "Main";

            workflowDesigners.Add(border);
        }
    }
}
