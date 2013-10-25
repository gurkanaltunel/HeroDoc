using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    interface ICommand
    {
        void Execute(object[] parameters); //bu method a object array parametre olarak gelecek.
    }
}
