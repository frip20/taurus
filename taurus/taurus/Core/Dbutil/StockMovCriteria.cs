using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using System.Runtime.Serialization;

namespace taurus.Core.Dbutil
{
    [DataContract]
    public class StockMovCriteria : IDateCriteria, IStockCriteria
    {
        [DataMember]
        public DateTime startDate { get; set; }
        [DataMember]
        public DateTime endDate { get; set; }
        [DataMember]
        public Proveedor Proveedor { get; set; }
        [DataMember]
        public string Factura { get; set; }
    }
}