using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MyDocuWithCommand
{
    public class Dir : ICommand
    {
        public void Execute()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:45704/Home/Index");
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //:todo
        }
    }
}
