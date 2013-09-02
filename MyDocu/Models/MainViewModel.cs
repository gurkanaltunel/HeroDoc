using System.Collections.Generic;
using DocumentService.Models;

namespace MyDocu.Models
{
    public class MainViewModel
    {
        public IList<Folder> Folders { get; set; }
        public FolderInformation CurrentFolder { get; set; }
    }
}