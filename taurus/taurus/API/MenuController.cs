using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using taurus.Core.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.API
{
    public class MenuController : ApiController
    {
        public readonly IUser _login;

        public MenuController(IUser login)
        {
            this._login = login;
        }

        public string Get() { return "1"; }

        public HttpResponseMessage Post(User user)
        {
            try
            {
                IList<taurus.Core.Entities.Action> actions = _login.actionsByUser(user.Id);
                return new TaurusResponseMessage(actions);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }

    }
}
