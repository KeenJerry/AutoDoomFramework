using AutoDoomFramework.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models
{

    class DProcess: Registry
    {
        private DependencyCollection dependencyCollection = new DependencyCollection();
        public DependencyCollection DependencyCollection
        {
            get => dependencyCollection;
            set => dependencyCollection = value;
        }

        private static Config config = new Config();
        public static Config Config
        {
            get => config;
            set => config = value;
        }

        private WorkflowCollection workflowCollection = new WorkflowCollection();
        public WorkflowCollection WorkflowCollection
        {
            get => workflowCollection;
            set => workflowCollection = value;
        }

        private FolderCollection folderCollection;
        public FolderCollection FolderCollection
        {
            get => folderCollection;
            set => folderCollection = value;
        }

        public Workflow MainWorkflow
        {
            get => WorkflowCollection.MainWorkflow;
        }

        private List<object> items = new List<object>();
        [JsonIgnore]
        public List<object> Items
        {
            get
            {
                items.Clear();
                items.Add(Config);
                items.AddRange(WorkflowCollection.Workflows);
                //items.Add(FolderCollection);
                items.Add(DependencyCollection);
                return items;
            }
        }

        public DProcess(): base() { }

        public DProcess(string Name, string Location) : base(Name, Location, "Process")
        {

        }

        public DProcess(string Name, string Location, string Description) : base(Name, Location, "Process", Description)
        {

        }
    }
}
