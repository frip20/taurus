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
    public class StockController : ApiController
    {
        public readonly IStock _stock;

        public StockController(IStock stock)
        {
            _stock = stock;
        }

        public HttpResponseMessage Get(int stockId)
        {
            Stock stock = _stock.searchObjectById(stockId);
            return new TaurusResponseMessage(stock);
        }

        public HttpResponseMessage Post(StockRequest stockRequest)
        {
            try
            {
                Stock stock = null;

                if (stockRequest.Action != APIActions.DELETE && stockRequest.Stock.Type == StockType.SALIDA) {
                    if (stockRequest.Stock.Uso != null && stockRequest.Stock.Uso.Id <= 0) {
                        stockRequest.Stock.Uso.SaveAndFlush();
                    }
                }

                if (stockRequest.Action == APIActions.ADD) {
                    _stock.addStock(stockRequest.Stock);
                }
                else if (stockRequest.Action == APIActions.UPDATE) {
                    _stock.updateStock(stockRequest.Stock);
                }
                else if (stockRequest.Action == APIActions.DELETE)
                {
                    _stock.deleteStock(stockRequest.Stock);
                }
                stock = stockRequest.Stock;

                if (stock != null)
                {
                    return new TaurusResponseMessage(stock.Id);
                }
                else {
                    return new TaurusResponseMessage(true, string.Format(MessageService.UNDEFINED_OBJECT, "Stock"));
                }
            }
            catch (InvalidUserException ex) {
                return new TaurusResponseMessage(true, ex.Message);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }

        }
    }
}