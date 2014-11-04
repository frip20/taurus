using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Exceptions
{
    public class CastleActivityException : TaurusException
    {
        public CastleActivityException(string message) : base(message) { }

        public CastleActivityException(string message, Exception ex) : base(message, ex) { }
    }
}