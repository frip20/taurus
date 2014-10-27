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
    public class EmpleadosController : ApiController
    {
        public readonly IEmpleado _empleado;

        public EmpleadosController(IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Empleado> emps = _empleado.searchByDescription(search);
                return new TaurusResponseMessage(emps);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}