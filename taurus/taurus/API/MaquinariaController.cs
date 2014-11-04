using System;
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
    public class MaquinariaController : ApiController
    {
        public readonly IMaquina _maquina;
        private readonly ICastleProvider _provider;

        public MaquinariaController(IMaquina maquina, ICastleProvider provider)
        {
            _maquina=maquina;
            _provider = provider;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Maquina> maqs = _maquina.getAllMaquinaria();
                return new TaurusResponseMessage(maqs);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(MaquinaRequest request)
        {
            try
            {
                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Maquina);
                        break;
                    case APIActions.DELETE:
                        request.Maquina.Enable = false;
                        _provider.Update(request.Maquina);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Maquina);
                        break;
                }
                return new TaurusResponseMessage(request.Maquina);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}