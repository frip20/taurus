using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Constants
{
    public enum EventType : int
    {
        SAVE_CASTLE_ITEM=100,
        UPDATE_CASTLE_ITEM = 101,
        DELETE_CASTLE_ITEM = 102,

        USER_LOGGING=500,
        ADD_NEW_USER=501,
        LOCK_USER=502,
        UNLOCK_USER=503,
        USER_LOGOUT=504
    }
}