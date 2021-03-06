﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using taurus.Core.Entities;
using taurus.Core.Interfaces;
using taurus.Core.Exceptions;
using taurus.Core.Web;
using taurus.Core.Services;
using taurus.Core.API;
using taurus.Core.Constants;


namespace taurus.API
{
    public class ConsumoController : ApiController
    {
        public readonly IConsumo _concepto;

        public ConsumoController(IConsumo concepto)
        {
            _concepto = concepto;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Consumo> conceptos = _concepto.getAllConceptos();
                return new TaurusResponseMessage(conceptos);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}