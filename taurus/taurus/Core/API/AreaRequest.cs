using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Constants;
using taurus.Core.Entities;

namespace taurus.Core.API
{
    public class AreaRequest : IApiRequest
    {
        public Area Area { get; set; }
        public APIActions Action { get; set; }
    }
}