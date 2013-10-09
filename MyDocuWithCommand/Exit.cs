using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class Exit:ICommand
    {
        public void Execute()
        {
            Environment.Exit(0);
        }


        public void ExecuteWithParameter(string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
