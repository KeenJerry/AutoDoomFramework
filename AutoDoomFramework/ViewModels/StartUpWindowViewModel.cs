using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Commands;
using AutoDoomFramework.Services.Interfaces;
using AutoDoomFramework.Models;

namespace AutoDoomFramework.ViewModels
{
    internal enum Function
    {
        Start,
        Help,
        Settings,
    }

    internal class StartUpWindowViewModel : BindableBase
    {
        private ICacheService cacheService;

        private bool startChecked = true;
        public bool StartChecked
        {
            get => startChecked;
            set
            {
                SetProperty(ref startChecked, value);
                RaisePropertyChanged(nameof(StartChecked));
            }
        }

        private bool helpChecked = false;
        public bool HelpChecked
        {
            get => helpChecked;
            set
            {
                SetProperty(ref helpChecked, value);
                RaisePropertyChanged(nameof(HelpChecked));
            }
        }

        private bool settingsChecked = false;
        public bool SettingsChecked
        {
            get => settingsChecked;
            set
            {
                SetProperty(ref settingsChecked, value);
                RaisePropertyChanged(nameof(SettingsChecked));
            }
        }

        private string startVisibility = "Visible";
        public string StartVisibility
        {
            get => startVisibility;
            set
            {
                SetProperty(ref startVisibility, value);
                RaisePropertyChanged(nameof(StartVisibility));
            }
        }

        private string helpVisibility = "Hidden";
        public string HelpVisibility
        {
            get => helpVisibility;
            set
            {
                SetProperty(ref helpVisibility, value);
                RaisePropertyChanged(nameof(helpVisibility));
            }
        }

        private string settingsVisibility = "Hidden";
        public string SettingsVisibility
        {
            get => settingsVisibility;
            set
            {
                SetProperty(ref settingsVisibility, value);
                RaisePropertyChanged(nameof(SettingsVisibility));
            }
        }

        private LinkedList<Registry> registries;
        public LinkedList<Registry> Registries
        {
            get => cacheService.GetRegistries();
            set
            {
                SetProperty(ref registries, value);
                RaisePropertyChanged(nameof(Registries));
            }
        }

        public StartUpWindowViewModel(ICacheService cacheService)
        {
            this.cacheService = cacheService;
            cacheService.LoadRecentRegistry();

            CheckCommand = new DelegateCommand<string>(Check, CanCheck);
        }

        // Delegate command
        public DelegateCommand<string> CheckCommand { get; private set; }
        private void Check(string function)
        {

            switch (function)
            {
                case "Start":
                    StartChecked = true;
                    HelpChecked = false;
                    SettingsChecked = false;

                    StartVisibility = "Visible";
                    HelpVisibility = "Hidden";
                    SettingsVisibility = "Hidden";
                    break;
                case "Help":
                    StartChecked = false;
                    HelpChecked = true;
                    SettingsChecked = false;

                    StartVisibility = "Hidden";
                    HelpVisibility = "Visible";
                    SettingsVisibility = "Hidden";
                    break;
                case "Settings":
                    StartChecked = false;
                    HelpChecked = false;
                    SettingsChecked = true;

                    StartVisibility = "Hidden";
                    HelpVisibility = "Hidden";
                    SettingsVisibility = "Visible";
                    break;
                default:
                    return;
            }
        }
        private bool CanCheck(string function)
        {
            return true;
        }
    }
}
