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
    public class ProveedorController : ApiController
    {
        public readonly IProveedor _proveedor;

        public ProveedorController(IProveedor proveedor) {
            _proveedor = proveedor;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Proveedor> proveedores = _proveedor.searchByNameOrRfc(search);
                return new TaurusResponseMessage(proveedores);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}