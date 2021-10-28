using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDoomFramework.Models.Project
{
    class FolderCollection
    {
        private List<Folder> folders = new List<Folder>();
        public List<Folder> Folders
        {
            get => folders;
            set => folders = value;
        }
    }
}
