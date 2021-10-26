using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using AutoDoomFramework.Common.Events;
using System.Threading;

namespace AutoDoomFramework.ViewModels
{
    class LoadingWindowViewModel: BindableBase
    {
        private bool loaded = false;
        public bool Loaded
        {
            get => loaded;
            set => SetProperty(ref loaded, value);
        }

        private bool initalProcessFailed = false;
        public bool InitialProcessFailed
        {
            get => initalProcessFailed;
            set => SetProperty(ref initalProcessFailed, value);
        }

        private string details = "Loading program...";
        public string Details
        {
            get => details;
            set
            {
                SetProperty(ref details, value);
                RaisePropertyChanged(nameof(Details));
            }
        }

        public void OnEditorLoaded()
        {
            Loaded = true;
        }

        public void OnInitialProcessFailed(string details)
        {
            Details = details;
            Thread.Sleep(3000);

            InitialProcessFailed = true;
        }

        public LoadingWindowViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<EditorLoadedEvent>().Subscribe(OnEditorLoaded);
            eventAggregator.GetEvent<InitalProcessFailedEvent>().Subscribe(OnInitialProcessFailed);
        }
    }
}
