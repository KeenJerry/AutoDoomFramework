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
using AutoDoomFramework.Models.Project;
using System.Activities.Presentation;
using System.Activities.Statements;

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

            string registriesSerialized = JsonSerializer.Serialize(registries.Values.Reverse(), new JsonSerializerOptions { WriteIndented = true });
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

        public bool InitialProjectFiles(ref Registry registry)
        {
            switch (registry)
            {
                case DProcess process:
                    {
                        if (File.Exists(Path.Combine(process.Location, process.Name, Config.ConfigFileName)))
                        {
                            return false;
                        }

                        foreach (Workflow workflow in process.WorkflowCollection.Workflows)
                        {
                            if (File.Exists(Path.Combine(process.Location, process.Name, workflow.FileName)))
                            {
                                return false;
                            }
                        }

                        // Create project directory
                        Directory.CreateDirectory(Path.Combine(process.Location, process.Name));
                        
                        // Create workflow files
                        foreach (Workflow workflow in process.WorkflowCollection.Workflows)
                        {
                            App.Current.Dispatcher.Invoke(() =>
                            {
                                WorkflowDesigner wf = new WorkflowDesigner();
                                Sequence sequence = new Sequence
                                {
                                    DisplayName = "Main"
                                };
                                wf.Load(sequence);
                                wf.Save(Path.Combine(process.Location, process.Name, workflow.FileName));
                            });
                        }
                        using (FileStream projectJson = File.Create(Path.Combine(process.Location, process.Name, Config.ConfigFileName)))
                        {
                            string content = JsonSerializer.Serialize(process, new JsonSerializerOptions { WriteIndented = true });
                            byte[] bytes = Encoding.UTF8.GetBytes(content);
                            projectJson.Write(bytes, 0, bytes.Length);
                        }

                        return true;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        public Registry FindRegistry(string uId)
        {
            foreach(Registry registry in registries.Values)
            {
                if (registry.UId.Equals(uId))
                {
                    registries.Put(registry);
                    return registry;
                }
            }

            return null;
        }

        public ref Registry GetWorkingRegistry()
        {
            return ref workingRegistry;
        }

        public CacheServiceImpl(IAppService appService)
        {
            this.appService = appService;
        }
    }
}
