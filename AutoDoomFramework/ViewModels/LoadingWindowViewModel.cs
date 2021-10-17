using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using AutoDoomFramework.Common.Events;

namespace AutoDoomFramework.ViewModels
{
    class LoadingWindowViewModel: BindableBase
    {
        private bool loaded = false;

        public bool Loaded
        {
            get => loaded;
            set
            {
                SetProperty(ref loaded, value);
            }
        }

        public void EditorLoaded()
        {
            Loaded = true;
        }

        public LoadingWindowViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<EditorLoadedEvent>().Subscribe(EditorLoaded);
        }
    }
}
