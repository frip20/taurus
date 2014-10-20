using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Articulos")]
    public class Articulo : CastleDesProvider<Articulo>
    {

        [Property]
        public string Parte { get; set; }

        [BelongsTo("categoriaid")]
        public Categoria Categoria { get; set; }

        [BelongsTo("unidadid")]
        public Unidad Unidad { get; set; }
    }
}