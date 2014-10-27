using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Constants
{
    [Serializable]    
    public enum APIActions : int
    {
        ADD=1,
        DELETE=2,
        UPDATE=3,
        LOCK=4,
        UNLOCK=5,
        SEARCHBYID=6
    }
}