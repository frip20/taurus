using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Requisiciones")]
    public class Requisicion : CastleStatusProvider<Requisicion>
    {
        [Property]
        public DateTime createDate { get; set; }

        [Property]
        public string tiempoEntrega { get; set; }

        [BelongsTo("Departamento")]
        public Departamento Departamento { get; set; }

        [BelongsTo("Uso")]
        public Uso Uso { get; set; }

        [BelongsTo("Responsable")]
        public Empleado Responsable { get; set; }

        [BelongsTo("Destino")]
        public Destino Destino { get; set; }

    }
}