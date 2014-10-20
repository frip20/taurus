using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Roles")]
    public class Rol : CastleDesProvider<Rol>
    {
        [HasAndBelongsToMany(typeof(Action), Table = "RolActions", ColumnKey = "idRol", ColumnRef = "idAction")]
        public IList<Action> Actions { get; set; }
    }
}