using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MyDocuWithCommand
{
    public class Dir:ICommand 
    {
        public void Execute()
        {
              
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:45704/Home/Index");
    request.ContentType = "application/json";
    try {
        WebResponse response = request.GetResponse();
        using (Stream responseStream = response.GetResponseStream()) {
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            Console.WriteLine(reader.ReadToEnd());
        }
    }
    catch (WebException ex) {
        WebResponse errorResponse = ex.Response;
        using (Stream responseStream = errorResponse.GetResponseStream())
        {
            StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            String errorText = reader.ReadToEnd();
            // log errorText
        }
        throw;
  }
    
            
        }
    }
}
