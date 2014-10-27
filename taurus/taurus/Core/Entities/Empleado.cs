using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Empleados")]
    public class Empleado : CastleProvider<Empleado>
    {
        [Property("name")]
        public string Description { get; set; }

        [Property]
        public string Puesto { get; set; }

        [BelongsTo("areaid")]
        public Area Area { get; set; }

        [Property]
        public string Email { get; set; }
    }
}