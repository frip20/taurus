using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Exceptions
{
    public class CastleActivityException : Exception
    {
        public CastleActivityException(string message, Exception ex) : base(message, ex) { }
    }
}