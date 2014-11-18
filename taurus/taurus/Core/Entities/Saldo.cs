using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Saldos")]
    public class Saldo : CastleProvider<Saldo>
    {
        [BelongsTo("articulo")]
        public Articulo Articulo { get; set; }

        [Property]
        public float Entradas { get; set; }

        [Property]
        public float Salidas { get; set; }

        [Property]
        public float Unitario { get; set; }
    }
}