using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Sistemas")]
    public class Sistema : CastleDesProvider<Sistema>
    {
        [Property]
        public string Clave { get; set; }
    }
}