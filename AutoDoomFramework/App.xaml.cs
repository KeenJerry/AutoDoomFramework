using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using AutoDoomFramework.Models;
using AutoDoomFramework.Views;
using AutoDoomFramework.Services.Interfaces;
using AutoDoomFramework.Services.Providers;
using Unity.Lifetime;

namespace AutoDoomFramework
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<StartUpWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppService, AppServiceImpl>();
            containerRegistry.RegisterSingleton<ICacheService, CacheServiceImpl>();
            containerRegistry.RegisterSingleton<IActivityService, ActivityServiceImpl>();
        }
    }
}
