using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentService.Abstractions;

namespace MyDocuWithCommand
{
    public interface ICommandFactory
    {
        void Specify();
    }
}
