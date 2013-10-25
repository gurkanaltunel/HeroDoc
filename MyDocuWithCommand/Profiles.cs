using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MyDocuWithCommand
{
    public class Profiles
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Profiles> profiles()
        {
            List<Profiles> profile = new List<Profiles>();
            RequestClass req = new RequestClass();
            string data = req.GetProfiles();
            JObject obj = JObject.Parse(data);
            JToken tok = obj["Profiles"];
            for (int i = 0; i < tok.Count(); i++)
            {

                profile.Add(new Profiles
                {
                    Id = (int)tok[i]["Id"],
                    Name = (string)tok[i]["Name"]
                });
            }
            return profile;
        }
    }
}
