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
    public class CategoriasController : ApiController
    {
        private readonly ICategoria _categoria;
        private readonly ICastleProvider _provider;

        public CategoriasController(ICategoria categoria, ICastleProvider provider)
        {
            _categoria = categoria;
            _provider = provider;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Categoria> cats = _categoria.getAllCategorias();
                return new TaurusResponseMessage(cats);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(CategoriaRequest request)
        {
            try
            {
                if (request.Action == APIActions.ADD || request.Action == APIActions.UPDATE)
                {
                    Categoria atemp = _categoria.getCategoriaByDescription(request.Categoria.Description);
                    if (atemp != null)
                    {
                        if (atemp.Id != request.Categoria.Id)
                        {
                            throw new CastleActivityException(string.Format(MessageService.CASTLE_DUPLICATE_ERROR, request.Categoria.Description));
                        }
                    }
                }

                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Categoria);
                        break;
                    case APIActions.DELETE:
                        request.Categoria.Enable = false;
                        _provider.Update(request.Categoria);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Categoria);
                        break;
                }
                return new TaurusResponseMessage(request.Categoria);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}