using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;
using taurus.Core.Constants;
using taurus.Core.Interfaces;

namespace taurus.Core.API
{
    public class UserRequest : IApiRequest
    {
        public User User { get; set; }
        public APIActions Action { get; set; }
    }
}