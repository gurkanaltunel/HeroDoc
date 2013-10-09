using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MyDocuWithCommand
{
    public class Dir:ICommand
    {
        public string Name { get; set; }
        public string ParentFolder { get; set; }
        public string CreateDate { get; set; }

        public Dir()
        {
           
        }
        public void Execute()
        {
            foreach (var item in Folders())
            {
                Console.Write(item.CreateDate+"\t");
                Console.Write(item.Name+"\t");
                if (item.ParentFolder==null)
                {
                    Console.Write("<DIR>");
                }
                Console.WriteLine();
            } 
        }
        public IList<Dir> Folders()
        {
            List<Dir> folders = new List<Dir>();
            RequestClass requestMethod = new RequestClass();
            string data = requestMethod.GetIndex();
            JObject jObject = JObject.Parse(data);
            JToken jDir = jObject["Folders"];
            for (int i = 0; i < jDir.Count(); i++)
            {
               string name = (string)jDir[i]["Name"];
               string createDate = (string)jDir[i]["CreateDate"];
               string parentFolder = (string)jDir[i]["ParentFolder"];
               Dir dir = new Dir
               {
                   Name = name,
                   CreateDate = createDate,
                   ParentFolder = parentFolder
               };
               folders.Add(dir);
            }
            return folders;
        }


        public void ExecuteWithParameter(string folderName)
        {
            throw new NotImplementedException();
        }
    }  
}
