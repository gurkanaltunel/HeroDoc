using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestfulRouting.Format;

namespace MyDocu.Controllers
{
    public abstract class ApplicationController:Controller
    {
        protected ActionResult RespondTo(Action<FormatCollection> format)
        {
            return new FormatResult(format);
        }
    }
}