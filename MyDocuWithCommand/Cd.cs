﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDocuWithCommand
{
    public class Cd:ICommand
    {

        public void Execute(object[] parameters)
        {
            RequestClass request = new RequestClass();
            request.ChangeDirectory(parameters[1].ToString());
        }
    }
}
