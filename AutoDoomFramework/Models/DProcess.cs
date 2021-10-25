using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models
{
    class DProcess: Registry
    {
        private List<string> Dependencies = new List<string> { "System.Activities" };

        public DProcess(string Name, string Location) : base(Name, Location)
        {

        }

        public DProcess(string Name, string Location, string Description) : base(Name, Location, Description)
        {

        }
        
    }
}
