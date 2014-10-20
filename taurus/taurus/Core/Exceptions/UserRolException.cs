using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taurus.Core.Exceptions
{
    public class UserRolException : Exception
    {
        public UserRolException(string message) : base(message) {}

        public UserRolException(string message, Exception exception) : base(message, exception) { }
    }
}