using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace taurus.Core.Services
{
    public sealed class ConfigurationService
    {
        private static volatile ConfigurationService _service;
        private static object syncRoot = new Object();

        private ConfigurationService() { }

        public static ConfigurationService Instance
        {
            get
            {
                if (_service == null) 
                 {
                    lock (syncRoot) 
                    {
                        if (_service == null)
                            _service = new ConfigurationService();
                    }
                 }

                return _service;
            } 
       }

        public string getProperty(string propertyName) {
            if (ConfigurationManager.AppSettings[propertyName] != null)
                return ConfigurationManager.AppSettings[propertyName].ToString();
            else
                return "";
        }

        public int getPropertyAsInt(string propertyName) {
            int val = -1;
            string propVal = this.getProperty(propertyName);
            int.TryParse(propVal,out val);
            return val;
        }

        public bool getPropertyAsBoolean(string propertyName)
        {
            bool val = false;
            string propVal = this.getProperty(propertyName);
            bool.TryParse(propVal, out val);
            return val;
        }

    }
}