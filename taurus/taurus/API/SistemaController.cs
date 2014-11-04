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
using taurus.Core.Constants;
using taurus.Core.Exceptions;
using taurus.Core.Services;
using taurus.Core.API;

namespace taurus.API
{
    public class SistemaController : ApiController
    {
        public readonly ISistema _sistema;
        private readonly ICastleProvider _provider;

        public SistemaController(ISistema sistema, ICastleProvider provider)
        {
            _sistema = sistema;
            _provider = provider;
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

        public HttpResponseMessage Post(SistemaRequest request)
        {
            try
            {
                if (request.Action == APIActions.ADD || request.Action == APIActions.UPDATE)
                {
                    Sistema atemp = _sistema.getSistemaByDescription(request.Sistema.Description);
                    if (atemp != null)
                    {
                        if (atemp.Id != request.Sistema.Id)
                        {
                            throw new CastleActivityException(string.Format(MessageService.CASTLE_DUPLICATE_ERROR, request.Sistema.Description));
                        }
                    }
                }

                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Sistema);
                        break;
                    case APIActions.DELETE:
                        request.Sistema.Enable = false;
                        _provider.Update(request.Sistema);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Sistema);
                        break;
                }
                return new TaurusResponseMessage(request.Sistema);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}