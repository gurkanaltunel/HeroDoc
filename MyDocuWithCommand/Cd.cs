using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class Cd
    {
        public string FolderPath { get; set; }
        public Cd(string folderPath)
        {
            FolderPath = folderPath;
        }
    }
}
