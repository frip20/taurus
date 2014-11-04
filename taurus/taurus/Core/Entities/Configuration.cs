using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord("Configuration")]
    public class Configuration : CastleDesProvider<Configuration>
    {
        [Property]
        public string Value { get; set; }
    }
}