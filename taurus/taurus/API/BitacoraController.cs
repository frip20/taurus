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

namespace taurus.API
{
    public class BitacoraController : ApiController
    {
        private readonly IBitacora _bitacora;
        private readonly ICastleProvider _provider;

        public BitacoraController(IBitacora bitacora, ICastleProvider provider)
        {
            _bitacora = bitacora;
            _provider = provider;
        }

        public HttpResponseMessage Get(int maquinaId)
        {
            try
            {
                IEnumerable<Bitacora> bits = _bitacora.getBitacoraByMaquina(maquinaId);
                return new TaurusResponseMessage(bits);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(BitacoraRequest  request)
        {
            try
            {
                switch (request.Action)
                {
                    case taurus.Core.Constants.APIActions.ADD:
                        _provider.Save(request.Bitacora);
                        break;
                    case taurus.Core.Constants.APIActions.DELETE:
                        _provider.Delete(request.Bitacora);
                        break;
                    case taurus.Core.Constants.APIActions.UPDATE:
                        _provider.Update(request.Bitacora);
                        break;
                }
               return new TaurusResponseMessage(request.Bitacora);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}