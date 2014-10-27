using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Conceptos")]
    public class Concepto : CastleDesProvider<Concepto>
    {
        [BelongsTo("sistema")]
        public Sistema Sistema { get; set; }
    }
}