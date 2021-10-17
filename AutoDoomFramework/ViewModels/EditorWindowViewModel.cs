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

        public EditorWindowViewModel(ICacheService cacheService)
        {
            this.cacheService = cacheService;
            RegistryName = cacheService.GetWorkingRegistryName();
        }
    }
}
