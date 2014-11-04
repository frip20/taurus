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
    public class AreasController : ApiController
    {
        private readonly IArea _area;
        private readonly ICastleProvider _provider;

        public AreasController(IArea area, ICastleProvider provider) {
            _area = area;
            _provider = provider;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IEnumerable<Area> areas = _area.getAllAreas();
                return new TaurusResponseMessage(areas);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(AreaRequest request)
        {
            try
            {
                if (request.Action == APIActions.ADD || request.Action == APIActions.UPDATE) {
                    Area atemp = _area.getAreaByDescription(request.Area.Description);
                    if (atemp != null) {
                        if (atemp.Id != request.Area.Id) {
                            throw new CastleActivityException(string.Format(MessageService.CASTLE_DUPLICATE_ERROR, request.Area.Description));
                        }
                    }
                }

                switch (request.Action)
                {
                    case APIActions.ADD:
                        _provider.Save(request.Area);
                        break;
                    case APIActions.DELETE:
                        request.Area.Enable = false;
                        _provider.Update(request.Area);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.Area);
                        break;
                }
                return new TaurusResponseMessage(request.Area);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}