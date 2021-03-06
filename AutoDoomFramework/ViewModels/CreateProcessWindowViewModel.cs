using AutoDoomFramework.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Prism.Commands;

using System.Windows;
using System.Threading;
using AutoDoomFramework.Services.Interfaces;
using Prism.Ioc;
using Prism.Events;
using AutoDoomFramework.Common.Events;

namespace AutoDoomFramework.ViewModels
{
    class CreateProcessWindowViewModel: BindableBase
    {
        private readonly ICacheService cacheService;
        private readonly IEventAggregator eventAggregator;

        private Registry process = new DProcess("BlankProcess", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        public DProcess Process
        {
            get => process as DProcess;
            set => process = value;
        }

        // Create button enabled
        private bool createButtonEnabled = true;
        public bool CreateButtonEnabled
        {
            get => createButtonEnabled;
            set
            {
                SetProperty(ref createButtonEnabled, value);
                RaisePropertyChanged(nameof(CreateButtonEnabled));
            }
        }

        private bool NameReady = true;
        private bool LocationReady = true;

        public DelegateCommand<string> NameTextChangeCommand { get; private set; }
        private void NameTextChange(string text)
        {
            NameReady = text != "";

            CreateButtonEnabled = NameReady && LocationReady;
            process.Name = text;
        }

        public DelegateCommand<string> LocationTextChangeCommand { get; private set; }
        private void LocationTextChange(string text)
        {
            LocationReady = text != "";

            CreateButtonEnabled = NameReady && LocationReady;
            process.Location = text;
        }

        public DelegateCommand<string> DescriptionTextChangeCommand { get; private set; }
        private void DescriptionTextChange(string text)
        {
            process.Description = text;
        }

        public DelegateCommand LoadEditorCommand { get; private set; }
        private void LoadEditor()
        {
            Thread loadingThread = new Thread(() => {
                cacheService.SetWorkingRegistry(ref process);
                //Thread.Sleep(3000);
                eventAggregator.GetEvent<EditorLoadedEvent>().Publish();
            });

            loadingThread.Start();
        }

        public CreateProcessWindowViewModel(IContainerExtension container, IEventAggregator eventAggregator)
        {
            NameTextChangeCommand = new DelegateCommand<string>(NameTextChange);
            LocationTextChangeCommand = new DelegateCommand<string>(LocationTextChange);
            DescriptionTextChangeCommand = new DelegateCommand<string>(DescriptionTextChange);
            LoadEditorCommand = new DelegateCommand(LoadEditor);

            cacheService = container.Resolve<ICacheService>();
            this.eventAggregator = eventAggregator;
        }
    }
}
