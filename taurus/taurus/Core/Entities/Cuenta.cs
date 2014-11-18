using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Cuentas")]
    public class Cuenta : CastleCompaqProvider<Cuenta>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Codigo { get; set; }

        [Property("Nombre")]
        public string Description { get; set; }
    }
}