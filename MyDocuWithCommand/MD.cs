using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            JsonClass jsonData = new JsonClass();
            jsonData.PostData(folderName);
        }
    }
}
