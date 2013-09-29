using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;

namespace MyDocuWithCommand
{
    public class CommandFactory:ICommandFactory
    {
        private InputCommand _input;

        public CommandFactory(InputCommand input)
        {
            _input = input;
        }
        public void Specify()
        {
            if (_input._commandName=="dir")
            {
                ICommand command = new Dir();
                command.Execute();
            }
            else if (_input._commandName=="md")
            {
                Md command = new Md(_input._paremeter);
                command.Execute();
            }
        }
    }
}
