using System.Collections.Generic;
using DocumentService.Models;

namespace MyDocu.Models
{
    public class AddFileModel
    {
        public IList<Profile> Profiles { get; set; }
        public AddFileModel()
        {
            Profiles = new List<Profile>();
        }

    }
}