using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class DependencyCollection
    {
        private string collectionName;
        public string CollectionName
        {
            get => collectionName;
            set => collectionName = value;
        }

        private List<Dependency> dependencies = new List<Dependency>() {};

        private List<DependencyCollection> dependencyCollections = new List<DependencyCollection>();
        public List<DependencyCollection> DependencyCollections 
        {   get => dependencyCollections;
            set => dependencyCollections = value; 
        }

        public List<Dependency> Dependencies
        {
            get => dependencies;
            set => dependencies = value;
        }

        private List<Object> items = new List<object>();
        [JsonIgnore]
        public List<Object> Items
        {
            get
            {
                items.Clear();
                items.AddRange(dependencies);
                items.AddRange(dependencyCollections);

                return items;
            }
        }

        public DependencyCollection() { }

        public DependencyCollection(string collectionName)
        {
            CollectionName = collectionName;
        }

        public DependencyCollection(string collectionName, string[] subCollectionNames)
        {
            CollectionName = collectionName;

            foreach(string name in subCollectionNames)
            {
                dependencyCollections.Add(new DependencyCollection(name));
            }

        }

        public DependencyCollection(string collectionName, string[] subCollectionNames, string[] dependencyNames)
        {
            CollectionName = collectionName;

            foreach (string name in subCollectionNames)
            {
                dependencyCollections.Add(new DependencyCollection(name));
            }

            foreach (string name in dependencyNames)
            {
                Dependencies.Add(new Dependency(name));
            }
        }

    }
}
