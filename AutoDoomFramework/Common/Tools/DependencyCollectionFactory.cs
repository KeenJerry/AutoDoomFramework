using AutoDoomFramework.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Common.Tools
{
    class DependencyCollectionFactory
    {
        public static DependencyCollection DefaultDependencyCollection()
        {
            DependencyCollection dependencyCollection = new DependencyCollection("Dependency", new string[] { "AutoDoom" });

            dependencyCollection.DependencyCollections.Find(dependency => dependency.CollectionName == "AutoDoom").Dependencies = new List<Dependency>
            {
                new Dependency("OCR"), new Dependency("Element"), new Dependency("Dial"), new Dependency("Render")
            };

            return dependencyCollection;
        }
    }
}
