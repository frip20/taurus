using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Constants;
using taurus.Core.Entities;

namespace taurus.Core.Services
{
    public sealed class AuditService
    {
        private static volatile AuditService _service;
        private static object syncRoot = new Object();

        private AuditService() { }

        public static AuditService Instance
        {
            get
            {
                if (_service == null)
                {
                    lock (syncRoot)
                    {
                        if (_service == null)
                            _service = new AuditService();
                    }
                }

                return _service;
            }
        }

        public void registerEvent(EventType eventType, int userId, int entityId, string eventInfo)
        {
            AuditEvent evnt = new AuditEvent() { 
                eventType = eventType,
                userId = userId,
                entityId = entityId,
                eventInfo = eventInfo,
                createDate = DateTime.Now
            };

            evnt.SaveAndFlush();
            evnt = null;
        }

        public void registerEvent(EventType eventType, int userId, string eventInfo)
        {
            registerEvent(eventType, userId, 0, eventInfo);
        }

    }
}