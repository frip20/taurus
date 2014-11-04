using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using taurus.Core.Constants;

namespace taurus.Core.Entities
{
    [ActiveRecord(table: "AuditEvents")]
    public class AuditEvent : ActiveRecordBase<AuditEvent>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public EventType eventType { get; set; }

        [Property]
        public int userId { get; set; }

        [Property]
        public int entityId { get; set; }

        [Property]
        public string eventInfo { get; set; }

        [Property]
        public DateTime createDate { get; set; }
    }
}