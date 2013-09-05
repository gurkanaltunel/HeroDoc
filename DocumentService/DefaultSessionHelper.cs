using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DocumentService.Abstractions;
using DocumentService.Models;

namespace DocumentService
{
    public class DefaultSessionHelper:ISessionHelper
    {
        private File _currentFile;

        public UserContext CurrentUser
        {
            get { return Get<UserContext>("context"); }
            private set { Set("context", value); }
        }

        private static TObjectType Get<TObjectType>(string key)
        {
            return (TObjectType)HttpContext.Current.Session[key];
        }
        private static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public FolderInformation CurrentFolder
        {
            get
            {
                return Get<FolderInformation>("currentFolder");
            }
            set
            {
                Set("currentFolder", value);
            }
        }

        public string CurrentFolderPath
        {
            get;
            set;
        }

        public File CurrentFile
        {
            get
            {
                return Get<File>("currentFile");
            }
            set
            {
                Set("currentFile", value);
            }
        }

        public void CreateLogin(UserContext context)
        {
            context.LoginTime = DateTime.Now;
            CurrentUser = context;
        }
    }
}
