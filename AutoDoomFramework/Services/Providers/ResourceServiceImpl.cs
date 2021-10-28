using AutoDoomFramework.Properties;
using AutoDoomFramework.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml;
using System.Xml;

namespace AutoDoomFramework.Services.Providers
{
    class ResourceServiceImpl : IResourceService
    {
        private string WorkflowDesignerColor { get; set; }

        public string GetWorkflowDesignerResource()
        {
            return WorkflowDesignerColor;
        }

        public void LoadWorkflowDesignerResource()
        {
            string resourceStr = File.ReadAllText(Resources.WorkflowDesignerResourceFile);
            StringReader reader = new StringReader(resourceStr);
            XmlReader xmlReader = XmlReader.Create(reader);
            ResourceDictionary resource = System.Windows.Markup.XamlReader.Load(xmlReader) as ResourceDictionary;

            Hashtable hashtable = new Hashtable();
            foreach(object key in resource.Keys)
            {
                hashtable.Add(key, resource[key]);
            }

            WorkflowDesignerColor = XamlServices.Save(hashtable);
        }
    }
}
