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
        private int _polizaid;

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

        [Property("polizaref")]
        public int PolizaId
        {
            get { return _polizaid; }
            set
            {
                _polizaid = value;
                if (value > 0)
                {
                    try
                    {
                        //_proveedor = Proveedor.Find(value);
                    }
                    catch { }
                }
            }
        }

        [Property]
        public string polizaConcepto { get; set; }

        [HasMany(typeof(StockItem), Table = "StockItems", ColumnKey = "stockid", Inverse = false, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Where="Enable=1", Lazy=false), ScriptIgnore]
        public IList<StockItem> Items { get; set; }

    }

    [ActiveRecord("stockitems")]
    public class StockItem : CastleProvider<StockItem>
    {
        private int _cuentaid;
        private Cuenta _cuenta;

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

        [Property]
        public int CuentaId
        {
            get { return _cuentaid; }
            set
            {
                _cuentaid = value;
                if (value > 0)
                {
                    try
                    {
                        _cuenta = Cuenta.Find(value);
                    }
                    catch { }
                }
            }
        }

        public Cuenta Cuenta
        {
            get { return _cuenta; }
            set
            {
                _cuenta = value;
                if (value != null)
                {
                    _cuentaid = value.Id;
                }
            }
        }
    }
}