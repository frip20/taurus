using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using taurus.Core.Entities;
using taurus.Core.Interfaces;
using taurus.Core.Exceptions;
using taurus.Core.Web;
using taurus.Core.Services;

namespace taurus.API
{
    public class LogoutController : ApiController
    {
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.Session["uid"] != null)
            {
                HttpContext.Current.Session["uid"] = null;
                HttpContext.Current.Session["user"] = null;
            }
            return new TaurusResponseMessage("DONE");
        }
    }
}