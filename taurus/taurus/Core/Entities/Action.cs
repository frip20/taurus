using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace taurus.Core.Entities
{
    [ActiveRecord(table:"Actions")]
    public class Action : CastleDesProvider<Action>
    {
        [Property]
        public int Value { get; set; }

        [Property]
        public bool isRouter { get; set; }

        [Property]
        public string routePath {get;set;}

        [Property]
        public string iconPath { get; set; }

        [Property]
        public int Sorted { get; set; }

    }
}