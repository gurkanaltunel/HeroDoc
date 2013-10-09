using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;

namespace MyDocuWithCommand
{
    public class CommandFactory
    {
        private InputCommand _input;
        private static readonly Dictionary<string, ICommand> CommandList = new Dictionary<string, ICommand>()
        {
            {"dir", new Dir()},
            {"md",new Md()},
            {"exit",new Exit()}
        };
        public CommandFactory(InputCommand input)
        {
            _input = input;
        }
       
        internal static ICommand GetCommand(string inputcommand)
        {
            var arguments = inputcommand.Split(' ');

            if (arguments != null && arguments.Length > 0)
            {
                var keyArgument = arguments[0];
                if (CommandList.ContainsKey(keyArgument))
                {
                    return CommandList[keyArgument];
                }
                else
                {
                    throw new NoCommandWasFoundException(keyArgument);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }
    }
}
