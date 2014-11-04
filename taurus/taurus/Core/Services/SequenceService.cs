using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Constants;

namespace taurus.Core.Services
{
    public sealed class SequenceService
    {
        private static volatile SequenceService _service;
        private static object syncRoot = new Object();

        private SequenceService() { }

        public static SequenceService Instance
        {
            get
            {
                if (_service == null)
                {
                    lock (syncRoot)
                    {
                        if (_service == null)
                            _service = new SequenceService();
                    }
                }

                return _service;
            }
        }

        public string getSequence(SequenceType type) {
            throw new NotImplementedException();
        }

    }
}