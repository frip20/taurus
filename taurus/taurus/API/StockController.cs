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

        public HttpResponseMessage Post(Stock stock)
        {
            try
            {
                if (stock != null)
                {
                    _stock.addStock(stock);
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