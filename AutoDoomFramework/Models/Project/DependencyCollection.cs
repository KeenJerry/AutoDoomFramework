using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class DependencyCollection
    {
        private string collectionName = "Dependency";
        public string CollectionName
        {
            get => collectionName;
            set => collectionName = value;
        }

        private List<Dependency> dependencies = new List<Dependency>() { };
        public List<Dependency> Dependencies
        {
            get => dependencies;
            set => dependencies = value;
        }

        public DependencyCollection() { }
    }
}
