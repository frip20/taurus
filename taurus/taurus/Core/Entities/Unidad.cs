using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Unidades")]
    public class Unidad : CastleDesProvider<Unidad>
    {
    }
}