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
    public class DepartamentoController : ApiController
    {
        public readonly IDepartamento _departamento;

        public DepartamentoController(IDepartamento departamento)
        {
            _departamento = departamento;
        }

        public HttpResponseMessage Get() {
            try
            {
                IEnumerable<Departamento> deps = _departamento.getAllDepartments();
                return new TaurusResponseMessage(deps);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}