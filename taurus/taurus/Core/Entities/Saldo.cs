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
        public decimal Entradas { get; set; }

        [Property]
        public decimal Salidas { get; set; }

        [Property]
        public decimal Unitario { get; set; }
    }
}