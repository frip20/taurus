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
    public class RolController : ApiController
    {
        public readonly IRolFactory _rol;

        public RolController(IRolFactory rol)
        {
            _rol = rol;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IList<Rol> roles = _rol.getAllRoles();
                return new TaurusResponseMessage(roles);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}