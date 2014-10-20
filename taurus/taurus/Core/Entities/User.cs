using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table:"Users")]
    public class User : CastleProvider<User>, IRole
    {
        [Property]
        public string userName { get; set; }
        [Property]
        public string Password { get; set; }

        [BelongsTo("roleid")]
        public Rol userRol { get; set; }
            
        
    }
}