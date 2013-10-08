using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyDocuWithCommand
{
    public class Md
    {
        public string folderName { get; set; }
        public Md(string parameter)
        {
            folderName = parameter;
        }

        public void Execute()
        {
            RequestClass requestMethod = new RequestClass();
            string response=requestMethod.CreateFolder(folderName);
            JObject obj=JObject.Parse(response);
            bool b = (bool)obj["ok"];  
            if (b)
            {
                Console.WriteLine(string.Format("Folder named {0} was created successfully"), folderName);
            }
            else
            {
                Console.WriteLine(string.Format("An occured the error while creating named {0}"), folderName);
            }
        }
    }
}
