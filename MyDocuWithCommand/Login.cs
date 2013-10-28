using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    class Login:ICommand
    {
        public void Execute(object[] parameters)
        {
            RequestClass request = new RequestClass();
            request.Login(parameters[1].ToString(), parameters[2].ToString());
        }
    }
}
