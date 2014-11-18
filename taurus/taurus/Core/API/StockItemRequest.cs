using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Constants;
using taurus.Core.Entities;

namespace taurus.Core.API
{
    public class StockItemRequest : IApiRequest
    {
        public StockItem StockItem { get; set; }
        public APIActions Action { get; set; }
        public StockType Type { get; set; }
        public int StockId { get; set; }
    }
}