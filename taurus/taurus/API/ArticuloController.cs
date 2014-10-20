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
    public class ArticuloController : ApiController
    {
        public readonly IArticulo _articulo;

        public ArticuloController(IArticulo articulo)
        {
            _articulo = articulo;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Articulo> articulos = _articulo.searchByNameOrPart(search);
                return new TaurusResponseMessage(articulos);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}