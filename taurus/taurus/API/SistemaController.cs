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
    public class SistemaController : ApiController
    {
        public readonly ISistema _sistema;

        public SistemaController(ISistema sistema)
        {
            _sistema = sistema;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Sistema> sistemas = _sistema.searchByNameOrClave(search);
                return new TaurusResponseMessage(sistemas);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}