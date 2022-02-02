using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoDoomFramework.Services.Interfaces;

namespace AutoDoomFramework.Services.Providers
{
    class AppServiceImpl: IAppService
    {
        private string InstallLocation { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AutoDoom"); } }
        private string CachePath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AutoDoom"); ; } }
        private string RegistryFileName { get { return "registry.json"; } }

        public AppServiceImpl()
        {
            InitAppStorage();
        }

        public string GetInstallLocation()
        {
            return InstallLocation;
        }

        private void InitAppStorage()
        {
            // Check install locatioin
            if (!Directory.Exists(InstallLocation))
            {
                Directory.CreateDirectory(InstallLocation);
            }

            // Check cache location
            if (!Directory.Exists(Path.Combine(InstallLocation, CachePath)))
            {
                Directory.CreateDirectory(Path.Combine(InstallLocation, CachePath));
                File.Create(Path.Combine(InstallLocation, CachePath, RegistryFileName));
            }
            else
            {
                if (!File.Exists(Path.Combine(InstallLocation, CachePath, RegistryFileName)))
                {
                    File.Create(Path.Combine(InstallLocation, CachePath, RegistryFileName));
                }
            }
        }

        public string GetRegistryLocation()
        {
            return Path.Combine(InstallLocation, CachePath, RegistryFileName);
        }
    }
}
