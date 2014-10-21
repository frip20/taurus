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

namespace taurus.API
{
    public class LoginController : ApiController
    {
        public readonly IUser _login;

        public LoginController(IUser login) {
            this._login = login;
        }

        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.Session["uid"] == null)
            {
                return new TaurusResponseMessage(true, "No session");
            }
            else {
                return new TaurusResponseMessage(HttpContext.Current.Session["uid"].ToString());
            }
        }

        public HttpResponseMessage Post(User user)
        {
            try
            {
                int uid = _login.validateUser(user.userName, user.Password);

                user = _login.searchObjectById(uid);
                
                //Save last access date
                user.lastAccessDate = DateTime.Now;
                user.SaveAndFlush();

                HttpContext.Current.Session["uid"] = uid;
                HttpContext.Current.Session["user"] = user;

                return new TaurusResponseMessage(user);
            }
            catch (InvalidUserException ex) {
                return new TaurusResponseMessage(true, ex.Message);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }

    }
}
