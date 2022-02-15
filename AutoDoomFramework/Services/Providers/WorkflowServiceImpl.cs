using AutoDoomFramework.Services.Interfaces;
using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Services.Providers
{
    class WorkflowServiceImpl : IWorkflowService
    {
        public WorkflowApplication WorkflowApp;

        private MemoryStream workflowStream;

        private ActivityXamlServicesSettings settings = new ActivityXamlServicesSettings()
        {
            CompileExpressions = true
        };

        private Activity activityExecute;

        private void Test()
        {
            Console.WriteLine("DDDDD");
        }

        public void Run(WorkflowDesigner wfDesigner)
        {
            workflowStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(wfDesigner.Text));
            activityExecute = ActivityXamlServices.Load(workflowStream, settings);
            WorkflowApp = new WorkflowApplication(activityExecute);
            WorkflowApp.Run();
        }

        public void Stop()
        {
            WorkflowApp.Terminate("User termination action.");
            workflowStream.Close();
        }
    }
}
