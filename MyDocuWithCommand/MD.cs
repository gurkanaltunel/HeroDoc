using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDocuWithCommand
{
    public class Md:ICommand
    {
        public Md()
        {
            
        }

        public void Execute(object[] parameters)
        {
            RequestClass requestMethod = new RequestClass();
            string response = requestMethod.CreateFolder(parameters[1].ToString());
            JObject obj = JObject.Parse(response);
            bool b = (bool)obj["ok"];
            if (b)
            {
                //Console.WriteLine(string.Format("Folder named {0} was created successfully"), folderName);
                Console.WriteLine("folder was created successfully");
            }
            else
            {
                Console.WriteLine(string.Format("An occured the error while creating named {0}"), parameters[1].ToString()); //parametre gelecek
            }
        }
    }
}
