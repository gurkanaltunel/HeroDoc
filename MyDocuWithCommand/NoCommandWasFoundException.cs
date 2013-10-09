using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDocuWithCommand
{
    public class NoCommandWasFoundException : Exception
    {
        public NoCommandWasFoundException(string keyArgument)
            : base(string.Format("{0} is not recognized as internal or external command,"
                                   +"operable program or batch file", keyArgument))
        {

        }
    }
}
