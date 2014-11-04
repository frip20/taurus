using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace taurus.Core.Services
{
    public sealed class LoggerService
    {
        private static volatile LoggerService _service;
        private static object syncRoot = new Object();
        private ILog logger;


        private LoggerService() {
            log4net.Config.XmlConfigurator.Configure();
            this.logger = LogManager.GetLogger("Taurus");
        }

        public static LoggerService Instance
        {
            get
            {
                if (_service == null)
                {
                    lock (syncRoot)
                    {
                        if (_service == null)
                            _service = new LoggerService();
                    }
                }

                return _service;
            }
        }

        public ILog Logger {
            get { return logger; }
        }

    }
}