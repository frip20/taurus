using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Areas")]
    public class Area : CastleProvider<Area>
    {
        [BelongsTo("departamento")]
        public Departamento Departamento { get; set; }

        [Property(Unique = true)]
        public string Description { get; set; }
    }
}