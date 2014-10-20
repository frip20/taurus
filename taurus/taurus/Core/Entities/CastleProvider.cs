using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    public abstract class CastleProvider<T> : ActiveRecordBase<T>
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
        [Property]
        public virtual bool Enable { get; set; }
    }
}