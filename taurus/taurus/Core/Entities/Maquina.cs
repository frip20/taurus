using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Maquinaria")]
    public class Maquina : CastleDesProvider<Maquina>
    {
        [Property]
        public string Econo { get; set; }

        [BelongsTo("area")]
        public Area Area { get; set; }

        [BelongsTo("operador")]
        public Empleado Operador { get; set; }

        [Property]
        public string Placa { get; set; }

        [Property]
        public string Comments { get; set; }

        public string fullName {
            get { 
                return string.Format("{0} - {1} [{2}]", (Econo == null ? "" : Econo), Description, (Placa == null ? "" : Placa));  
            }
        }
    }
}