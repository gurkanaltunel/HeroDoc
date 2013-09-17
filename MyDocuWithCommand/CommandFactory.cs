using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class CommandFactory:ICommandFactory
    {
        private InputCommand _input;

        public CommandFactory(InputCommand input)
        {
            _input = input;
        }

        public void Execute()
        {
            CultureInfo culture=CultureInfo.CurrentCulture;

            if (_input._commandName.ToLower(culture)=="dir")
            {
                ICommand command = new DIR();
                command.Execute();
            }          
        }
    }
}
