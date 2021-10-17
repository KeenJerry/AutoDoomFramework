using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Services.Interfaces
{
    interface IAppService
    {
        string GetInstallLocation();

        string GetRegistryLocation();
    }
}
