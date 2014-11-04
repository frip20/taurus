using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Exceptions
{
    public class InvalidUserException : TaurusException
    {
        public InvalidUserException(string message) : base(message) {}

        public InvalidUserException(string message, Exception exception) : base(message, exception) { }
    }
}