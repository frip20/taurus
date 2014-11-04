using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Constants;
using taurus.Core.Entities;

namespace taurus.Core.API
{
    public class CategoriaRequest : IApiRequest
    {
        public Categoria Categoria { get; set; }
        public APIActions Action { get; set; }
    }
}