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
    }
}
