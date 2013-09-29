using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MyDocuWithCommand
{
    public class JsonClass
    {
        public string ReturnJson()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:45704/Home/Index");
            request.ContentType = "application/json";

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
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
                    // log errorText
                }
                throw;
            }
        }
        public string PostData(string data)
        {
            //string uri="http://localhost:45704/Home/CreateFolder";
            //using (var client = new WebClient())
            //{
            //    client.Headers[HttpRequestHeader.ContentType] = "application/json";
            //    string result=client.UploadString(uri, "POST", data);
            //    return result;
            //}
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:45704/Home/CreateFolder");
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials; ;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Headers["X-Accept"] = "application/json; charset=utf-8";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }
    }
}
