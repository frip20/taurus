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


namespace taurus.API
{
    public class EmpleadosController : ApiController
    {
        public readonly IEmpleado _empleado;
        private readonly ICastleProvider _provider;

        public EmpleadosController(IEmpleado empleado, ICastleProvider provider)
        {
            _empleado = empleado;
            _provider = provider;
        }

        public HttpResponseMessage Get(string search) {
            try
            {
                IEnumerable<Empleado> emps = _empleado.searchByDescription(search);
                return new TaurusResponseMessage(emps);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(EmpleadoRequest request)
        {
            try
            {
                if (request.Action == APIActions.ADD || request.Action == APIActions.UPDATE)
                {
                    //Area atemp = _area.getAreaByDescription(request.Area.Description);
                    //if (atemp != null)
                    //{
                    //    if (atemp.Id != request.Area.Id)
                    //    {
                    //        throw new CastleActivityException(string.Format(MessageService.CASTLE_DUPLICATE_ERROR, request.Area.Description));
                    //    }
                    //}
                }

                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Empleado);
                        break;
                    case APIActions.DELETE:
                        request.Empleado.Enable = false;
                        _provider.Update(request.Empleado);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Empleado);
                        break;
                    case APIActions.CUSTOMSEARCH:
                        return new TaurusResponseMessage(_empleado.filterBy(request.Empleado));
                }
                return new TaurusResponseMessage(request.Empleado);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}