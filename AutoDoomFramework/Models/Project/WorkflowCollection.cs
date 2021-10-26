using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoDoomFramework.Models.Project
{
    class WorkflowCollection
    {
        private List<Workflow> workflows = new List<Workflow> { new Workflow("Main.xaml", true) };
        public List<Workflow> Workflows
        {
            get => workflows;
            set => workflows = value;
        }

        [JsonIgnore]
        public Workflow MainWorkflow
        {
            get
            {
                foreach (Workflow workflow in Workflows)
                {
                    if (workflow.IsMain)
                    {
                        return workflow;
                    }
                }
                return null;
            }
        }

        public WorkflowCollection() { }
    }
}
