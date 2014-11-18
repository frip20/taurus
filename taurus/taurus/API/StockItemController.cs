using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using taurus.Core.Interfaces;
using System.Net.Http;
using taurus.Core.Web;
using taurus.Core.API;
using taurus.Core.Constants;
using taurus.Core.Entities;

namespace taurus.API
{
    public class StockItemController : ApiController
    {
        private readonly IStock _stock;
        private readonly ICastleProvider _provider;

        public StockItemController(IStock stock, ICastleProvider provider)
        {
            _stock = stock;
            _provider = provider;
        }

        public HttpResponseMessage Post(StockItemRequest request)
        {
            try
            {
                request.StockItem.Stock = new Stock() { Id = request.StockId };
                switch (request.Action)
                {
                    case APIActions.DELETE:
                        request.StockItem.Enable = false;
                        _provider.Update(request.StockItem);
                        _stock.afectarSaldo(request.StockItem, request.Type);
                        break;
                    case APIActions.UPDATE:
                        _provider.Update(request.StockItem);
                        _stock.afectarSaldo(request.StockItem, request.Type);
                        break;
                }
                return new TaurusResponseMessage(request.StockItem);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}