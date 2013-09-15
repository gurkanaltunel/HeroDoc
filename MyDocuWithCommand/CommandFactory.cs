using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class CommandFactory:ICommand
    {
        private InputCommand _input;
        private Command command;

        public CommandFactory(InputCommand input)
        {
            _input = input;
        }

        public void Execute()
        {
            command.Execute();
        }
    }
}
