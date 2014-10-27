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
    public class UsosController : ApiController
    {
        public readonly IUso _uso;

        public UsosController(IUso uso)
        {
            _uso = uso;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Uso> usos = _uso.searchByDescription(search);
                return new TaurusResponseMessage(usos);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}