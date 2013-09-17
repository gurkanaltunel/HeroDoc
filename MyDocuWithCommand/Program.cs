using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;

namespace MyDocuWithCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = Console.ReadLine();
            string parameter = Console.ReadLine();
            InputCommand input = new InputCommand(command, parameter);
            ICommandFactory comfactory = new CommandFactory(input);
            comfactory.Execute();
        }
    }
}
