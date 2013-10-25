using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MyDocuWithCommand
{
    public class Copy:ICommand
    {
        public void Execute(object[] parameters)
        {
            if (parameters.Length<2)
            {
                Profiles profiles = new Profiles();
                foreach (var item in profiles.profiles())
                {
                    Console.Write(item.Id + "\t");
                    Console.Write(item.Name + "\t");
                    Console.WriteLine();
                }
            }
            else
            {
                RequestClass req = new RequestClass();
                string result=req.CreateFile(int.Parse(parameters[1].ToString()), parameters[2].ToString());
                JObject obj = JObject.Parse(result);
                bool b = (bool)obj["ok"];
                if (b)
                {
                    Console.WriteLine("file was uploaded successfully");
                }          
            }        
        }
    }
}
