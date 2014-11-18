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
using taurus.Core.Exceptions;
using taurus.Core.Services;
using taurus.Core.Constants;
using taurus.Core.API;
namespace taurus.API
{
    public class ArticuloController : ApiController
    {
        public readonly IArticulo _articulo;
        private readonly ICastleProvider _provider;

        public ArticuloController(IArticulo articulo, ICastleProvider provider)
        {
            _articulo = articulo;
            _provider = provider;
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

        public HttpResponseMessage Post(ArticuloRequest request)
        {
            try
            {
                if (request.Action == APIActions.ADD || request.Action == APIActions.UPDATE)
                {
                    Articulo atemp = _articulo.getArticuloByDescription(request.Articulo.Description);
                    if (atemp != null)
                    {
                        if (atemp.Id != request.Articulo.Id)
                        {
                            throw new CastleActivityException(string.Format(MessageService.CASTLE_DUPLICATE_ERROR, request.Articulo.Description));
                        }
                    }
                }

                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Articulo);
                        break;
                    case APIActions.DELETE:
                        request.Articulo.Enable = false;
                        _provider.Update(request.Articulo);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Articulo);
                        break;
                    case APIActions.CUSTOMSEARCH:
                        return new TaurusResponseMessage(_articulo.filterBy(request.Articulo));
                }
                return new TaurusResponseMessage(request.Articulo);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}