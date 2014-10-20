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
using taurus.Core.Dbutil;
namespace taurus.API
{
    public class StockMovController : ApiController
    {
        public readonly IStock _stock;

        public StockMovController(IStock stock)
        {
            _stock = stock;
        }

        public HttpResponseMessage Post(StockMovCriteria criteria)
        {
            try
            {
                var items = _stock.getStockMovs(criteria);
                return new TaurusResponseMessage(items);
            }
            catch (Exception ex) {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }
    }
}