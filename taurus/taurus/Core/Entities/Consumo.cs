using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Consumos")]
    public class Consumo : CastleDesProvider<Consumo>
    {
        [BelongsTo("sistema")]
        public Sistema Sistema { get; set; }
    }
}