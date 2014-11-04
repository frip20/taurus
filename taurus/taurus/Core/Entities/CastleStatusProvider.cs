using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using taurus.Core.Constants;

namespace taurus.Core.Entities
{
    public class CastleStatusProvider<T> : CastleProvider<T>
    {
        [BelongsTo("status")]
        public virtual Status Status { get; set; }
    }
}