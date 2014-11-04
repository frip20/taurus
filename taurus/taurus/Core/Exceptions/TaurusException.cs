using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Services;

namespace taurus.Core.Exceptions
{
    public abstract class TaurusException : Exception
    {
        public TaurusException(string message) : base(message) {
            LoggerService.Instance.Logger.Error(message);
        }

        public TaurusException(string message, Exception exception) : base(message, exception) {
            LoggerService.Instance.Logger.Error(message, exception);
        }
    }
}