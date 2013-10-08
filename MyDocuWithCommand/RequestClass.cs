using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MyDocuWithCommand
{
    public class RequestClass
    {
       private HttpWebRequest Request;
       private HttpWebResponse Response;

        public string GetIndex()
        {

            Request = (HttpWebRequest)WebRequest.Create("http://localhost:45704/Home/Index");
            Request.ContentType = "application/json";
           
            try
            {
                Response = (HttpWebResponse)Request.GetResponse();
                CookieCollection collect = Response.Cookies;
                
                using (Stream responseStream = Response.GetResponseStream())
                {
                    
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                }
                throw;
            }
             

        }
        public string CreateFolder(string data)
        {
            try
            {
                Request = (HttpWebRequest)WebRequest.Create(string.Format("http://localhost:45704/Home/CreateFolder?folder={0}",data)); 
                Request.UserAgent = ".NET Framework Client";
                Request.Proxy = WebRequest.DefaultWebProxy;
                Request.Credentials = System.Net.CredentialCache.DefaultCredentials; ;
                Request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                Request.Headers["X-Accept"] = "application/json; charset=utf-8";
                Request.Method = "POST";
                string jsonstring = JsonConvert.SerializeObject(data);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonstring);
                Request.ContentLength = byteArray.Length;
                Stream dataStream = Request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = Request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                return reader.ReadToEnd();

            }
            catch (Exception ex)
            {
                throw ex;
               
            }     
        }
    }
}
