using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Constants;

namespace taurus.Core.API
{
    public class EmpleadoRequest : IApiRequest
    {
        public Empleado Empleado { get; set; }
        public APIActions Action { get; set; }
    }
}