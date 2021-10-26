using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class Config
    {
        private static string configFileName = "Project.json";
        public static string ConfigFileName
        {
            get => configFileName;
            set => configFileName = value;
        }
    }
}
