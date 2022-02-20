using AutoDoomFramework.ViewModels;
using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Common.Tools
{
    class RunProcessTrackingParticipant : TrackingParticipant
    {
        private readonly EditorWindowViewModel editorWindowViewModel;

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            WorkflowInstanceRecord wir = record as WorkflowInstanceRecord;
            switch(wir.State)
            {
                case WorkflowInstanceStates.Started:
                    {
                        Console.WriteLine("Started");
                        editorWindowViewModel.PublishProcessStartEvent();
                        break;
                    }
                case WorkflowInstanceStates.Aborted:
                    {
                        Console.WriteLine("Aborted");
                        break;
                    }
                case WorkflowInstanceStates.Canceled:
                    {
                        Console.WriteLine("Canceled");
                        break;
                    }
                case WorkflowInstanceStates.Completed:
                    {
                        Console.WriteLine("Completed");
                        editorWindowViewModel.PublishProcessCompleteEvent();
                        break;
                    }
                case WorkflowInstanceStates.Deleted:
                    {
                        Console.WriteLine("Deleted");
                        break;
                    }
                case WorkflowInstanceStates.Idle:
                    {
                        Console.WriteLine("Idle");
                        break;
                    }
                case WorkflowInstanceStates.Persisted:
                    {
                        Console.WriteLine("Persisted");
                        break;
                    }
                case WorkflowInstanceStates.Resumed:
                    {
                        Console.WriteLine("Resumed");
                        break;
                    }
                case WorkflowInstanceStates.Suspended:
                    {
                        Console.WriteLine("Suspended");
                        break;
                    }
                case WorkflowInstanceStates.Terminated:
                    {
                        Console.WriteLine("Terminated");
                        editorWindowViewModel.PublishProcessTerminateEvent();
                        break;
                    }
                case WorkflowInstanceStates.UnhandledException:
                    {
                        Console.WriteLine("UnhandledException");
                        break;
                    }
                case WorkflowInstanceStates.Unloaded:
                    {
                        Console.WriteLine("Unloaded");
                        break;
                    }
                case WorkflowInstanceStates.Unsuspended:
                    {
                        Console.WriteLine("Unsuspended");
                        break;
                    }
                case WorkflowInstanceStates.Updated:
                    {
                        Console.WriteLine("Updated");
                        break;
                    }
                case WorkflowInstanceStates.UpdateFailed:
                    {
                        Console.WriteLine("UpdateFailed");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Defaulted");
                        break;
                    }
            }
        }

        public RunProcessTrackingParticipant(EditorWindowViewModel editWindowViewModel)
        {
            editorWindowViewModel = editWindowViewModel;
        }
    }
}
