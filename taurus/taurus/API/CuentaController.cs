using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using taurus.Core.Entities;
using taurus.Core.Web;
using taurus.Core.Interfaces;

namespace taurus.API
{
    public class CuentaController : ApiController
    {
        public readonly ICuenta _cuenta;

        public CuentaController(ICuenta cuenta)
        {
            _cuenta = cuenta;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Cuenta> cuentas = _cuenta.searchByNameOrCode(search);
                return new TaurusResponseMessage(cuentas);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}