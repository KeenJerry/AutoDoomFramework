using AutoDoomFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Services.Interfaces
{
    interface ICacheService
    {
        void FlushToCache();

        void AddRegistry(ref Registry registry);

        LinkedList<Registry> GetRegistries();

        void LoadRecentRegistry();

        void SetWorkingRegistry(ref Registry registry);

        string GetWorkingRegistryName();
    }
}
