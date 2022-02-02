using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class Dependency
    {
        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        public Dependency(string name)
        {
            Name = name;
        }
    }
}
