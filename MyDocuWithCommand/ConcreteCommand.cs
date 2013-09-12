using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class ConcreteCommand:CommandBase
    {
        public string Parametre { get; set; }

        public ConcreteCommand(Receiver receiver):base(receiver)
        {

        }

        public override void Execute()
        {
            _receiver.Action();
        }
    }
}
