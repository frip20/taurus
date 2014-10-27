using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "Departamentos")]
    public class Departamento : CastleDesProvider<Departamento>
    {
    }
}