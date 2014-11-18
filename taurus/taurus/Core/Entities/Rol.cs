using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using System.Web.Script.Serialization;

namespace taurus.Core.Entities
{
    [ActiveRecord("Roles")]
    public class Rol : CastleDesProvider<Rol>
    {
        [HasAndBelongsToMany(typeof(Action), Table = "RolActions", ColumnKey = "idRol", ColumnRef = "idAction"), ScriptIgnore]
        public IList<Action> Actions { get; set; }

        [HasAndBelongsToMany(typeof(Action), Table = "RolActions", ColumnKey = "idRol", ColumnRef = "idAction", Inverse = false, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Where = "action1_.routePath='ACTION'"), ScriptIgnore]
        public IList<Action> Permissions { get; set; }
    }
}