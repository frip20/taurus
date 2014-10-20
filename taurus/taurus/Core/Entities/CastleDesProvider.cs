using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    public class CastleDesProvider<T> : CastleProvider<T>
    {
        [Property]
        public virtual string Description { get; set; }
    }
}