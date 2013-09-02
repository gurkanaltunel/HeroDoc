using System.Collections.Generic;
using System.Linq;
using DocumentService.Models;

namespace MyDocu.Models
{
    public class OpenFileViewModel
    {
        public File File { get; set; }
        public IList<FileVersion> FileVersions { get; set; }
        public FileVersion LastFileVersion
        {
            get { return FileVersions.OrderBy(version => version.VersionNumber).Last(); } 
        }
    }
}