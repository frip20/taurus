using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Bitacora")]
    public class Bitacora : CastleProvider<Bitacora>
    {
        private int _proveedorid;
        private Proveedor _proveedor;

        [BelongsTo("maquina")]
        public Maquina Maquina { get; set; }

        [Property]
        public DateTime createDate { get; set; }

        [BelongsTo("consumo")]
        public Consumo Consumo { get; set; }

        [Property("proveedor")]
        public int ProveedorId
        {
            get { return _proveedorid; }
            set
            {
                _proveedorid = value;
                if (value > 0)
                {
                    try
                    {
                        _proveedor = Proveedor.Find(value);
                    }
                    catch { }
                }
            }
        }

        public Proveedor Proveedor
        {
            get { return _proveedor; }
            set
            {
                _proveedor = value;
                if (value != null)
                {
                    _proveedorid = value.Id;
                }
            }
        }

        [Property]
        public float Cantidad { get; set; }

        [Property]
        public float Costo { get; set; }

        [Property]
        public int Kms { get; set; }
    }
}