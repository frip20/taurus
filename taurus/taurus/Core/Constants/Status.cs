using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;
using Castle.ActiveRecord;

namespace taurus.Core.Constants
{
    [ActiveRecord(table: "Status")]
    public class Status : CastleDesProvider<Status>
    {

    }
}