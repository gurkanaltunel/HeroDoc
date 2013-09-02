using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Models;
namespace DocumentService.Abstractions
{
    public interface ISessionHelper
    {
        UserContext CurrentUser { get; }
        FolderInformation CurrentFolder { get; set; }
        string CurrentFolderPath { get; set; }
        File CurrentFile { get; set; }

        void CreateLogin(UserContext context);
    }
}
