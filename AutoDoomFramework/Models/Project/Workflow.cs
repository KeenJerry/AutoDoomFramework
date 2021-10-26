using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class Workflow
    {
        private string fileName;
        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        public bool IsMain { get; set; }

        public Workflow(string filename, bool isMain)
        {
            FileName = filename;
            IsMain = isMain;
        }
    }
}
