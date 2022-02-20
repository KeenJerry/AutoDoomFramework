using AutoDoomFramework.Common.Tools;
using AutoDoomFramework.Services.Interfaces;
using AutoDoomFramework.ViewModels;
using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Tracking;
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

        public void Run(WorkflowDesigner wfDesigner, EditorWindowViewModel editorWindowViewModel)
        {
            workflowStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(wfDesigner.Text));
            activityExecute = ActivityXamlServices.Load(workflowStream, settings);
            WorkflowApp = new WorkflowApplication(activityExecute);

            RunProcessTrackingParticipant trackingParticipant = new RunProcessTrackingParticipant(editorWindowViewModel)
            {
                TrackingProfile = new TrackingProfile
                {
                    Name = "AutoDoomWorkflowTracking",
                    ActivityDefinitionId = "RunningProcess",
                    ImplementationVisibility = ImplementationVisibility.RootScope,
                    Queries =
                    {
                        new WorkflowInstanceQuery
                        {
                            States = { "*" },
                        }
                    }
                }
            };

            WorkflowApp.Extensions.Add(trackingParticipant);
            WorkflowApp.Run();
        }

        public void Terminate()
        {
            if (WorkflowApp is null)
            {
                return;
            }
            WorkflowApp.Terminate("User termination action.");
            workflowStream.Close();
        }
    }
}
