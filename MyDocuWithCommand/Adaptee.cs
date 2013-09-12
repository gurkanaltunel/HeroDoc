using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver();
            ConcreteCommand command = new ConcreteCommand(receiver);
            command.Parametre = "";
            invoker.Command = command;
            invoker.ExecuteCommand();
        }
    }
}
