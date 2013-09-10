using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class Invoker
    {
        public CommandBase Command { get; set; }
        public void ExecuteCommand()
        {
            Command.Execute();
        }
    }
}
