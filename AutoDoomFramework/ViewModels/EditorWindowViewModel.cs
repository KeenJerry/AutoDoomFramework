using AutoDoomFramework.Models;
using AutoDoomFramework.Models.ToolBox;
using AutoDoomFramework.Services.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public EditorWindowViewModel(ICacheService cacheService, IActivityService activityService)
        {
            this.cacheService = cacheService;
            this.activityService = activityService;

            RegistryName = cacheService.GetWorkingRegistryName();

            activityService.LoadDefaultActivities();
        }
    }
}
