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
    public class UsersController : ApiController
    {
        public readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IList<User> users = _user.getAllUsers();
                return new TaurusResponseMessage(users);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}