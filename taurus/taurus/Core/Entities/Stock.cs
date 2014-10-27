using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using taurus.Core.Constants;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace taurus.Core.Entities
{
    [ActiveRecord("Stock")]
    public class Stock : CastleProvider<Stock>
    {
        private int _proveedorid;
        private Proveedor _proveedor;

        [Property]
        public StockType Type { get; set; }

        [Property]
        public int ProveedorId {
            get { return _proveedorid; }
            set {
                _proveedorid = value;
                if (value > 0) {
                    try
                    {
                        _proveedor = Proveedor.Find(value);
                    }
                    catch { }
                }
            }
        }

        public Proveedor Proveedor { 
            get { return _proveedor; }
            set { 
                _proveedor = value;
                if (value != null) {
                    _proveedorid = value.Id;
                }
            }
        }

        [Property]
        public string Factura { get; set; }

        [Property]
        public DateTime CreateDate { get; set; }

        [BelongsTo("departamentoid")]
        public Departamento Departamento { get; set; }

        [BelongsTo("uso")]
        public Uso Uso { get; set; }

        [BelongsTo("responsable")]
        public Empleado Responsable { get; set; }


        [HasMany(typeof(StockItem), Table = "StockItems", ColumnKey = "stockid", Inverse = false, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Where="Enable=1"), ScriptIgnore]
        public IList<StockItem> Items { get; set; }

    }

    [ActiveRecord("stockitems")]
    public class StockItem : CastleProvider<StockItem>
    {
        [BelongsTo("stockid"), JsonIgnore]
        public Stock Stock { get; set; }

        [BelongsTo("articuloid")]
        public Articulo Articulo { get; set; }

        [Property]
        public float Cantidad { get; set; }

        [Property]
        public float Unitario { get; set; }

        [BelongsTo("sistemaid")]
        public Sistema Sistema { get; set; }
    }
}