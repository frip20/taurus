using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    public abstract class CastleCompaqProvider<T> : ActiveRecordBase<T>
    {
    }
}