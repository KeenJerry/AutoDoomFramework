using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

using AutoDoomFramework.Common.Tools;
using AutoDoomFramework.Models;
using AutoDoomFramework.Services.Interfaces;

namespace AutoDoomFramework.Services.Providers
{
    class CacheServiceImpl : ICacheService
    {

        private LRU<Registry> registries = new LRU<Registry>();

        private Registry workingRegistry = null;

        private IAppService appService;

        public void AddRegistry(ref Registry registry)
        {
            registries.Put(registry);
        }

        public void FlushToCache()
        {

            string registriesSerialized = JsonSerializer.Serialize(registries.Values);
            File.WriteAllText(appService.GetRegistryLocation(), registriesSerialized);
        }

        public LinkedList<Registry> GetRegistries()
        {
            return registries.Values;
        }

        public void LoadRecentRegistry()
        {
            string registriesSerialized = File.ReadAllText(appService.GetRegistryLocation());
            if (registriesSerialized is "")
            {
                return;
            }
            LinkedList<Registry> registryList = JsonSerializer.Deserialize<LinkedList<Registry>>(registriesSerialized);
            registries.Clear();
            foreach (Registry registry in registryList)
            {
                registries.Put(registry);
            }
        }

        public void SetWorkingRegistry(ref Registry registry)
        {
            workingRegistry = registry;
        }

        public string GetWorkingRegistryName()
        {
            return workingRegistry is null ? "" : workingRegistry.Name;
        }

        public CacheServiceImpl(IAppService appService)
        {
            this.appService = appService;
        }
    }
}
