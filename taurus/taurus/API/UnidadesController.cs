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
using taurus.Core.API;
using taurus.Core.Constants;
using taurus.Core.Services;
using taurus.Core.Exceptions;


namespace taurus.API
{
    public class UnidadesController : ApiController
    {
        private readonly IUnidad _unidad;
        private readonly ICastleProvider _provider;

        public UnidadesController(IUnidad unidad, ICastleProvider provider)
        {
            _unidad = unidad;
            _provider = provider;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Unidad> unids = _unidad.getAllUnidades();
                return new TaurusResponseMessage(unids);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}