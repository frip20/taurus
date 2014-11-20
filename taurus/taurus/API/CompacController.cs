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
    public class CompacController : ApiController
    {
        private readonly ICompac _compac;

        public CompacController(ICompac compac)
        {
            _compac = compac;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                return new TaurusResponseMessage("");
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(CompacRequest request)
        {
            try
            {
                if (request.Stock.Type == StockType.ENTRADA)
                {
                    _compac.polizaEntrada(request.Stock);
                }
                else {
                    _compac.polizaSalida(request.Stock);
                }

                return new TaurusResponseMessage(request.Stock);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }

    }
}