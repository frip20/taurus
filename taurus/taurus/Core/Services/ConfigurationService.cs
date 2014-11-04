using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;


namespace taurus.Core.Services
{
    public sealed class ConfigurationService
    {
        private static volatile ConfigurationService _service;
        private static object syncRoot = new Object();
        private IEnumerable<Configuration> configs;

        private ConfigurationService() {
            configs = Configuration.FindAll().AsEnumerable<Configuration>();
        }

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

        private Configuration getConfigurationByName(string name) {
            return configs.Where(c => c.Description.ToLower() == name.ToLower()).First();
        }

        public string getProperty(string propertyName) {
            return getProperty(propertyName, null);
        }

        public string getProperty(string propertyName, string defaultValue) {
            Configuration config = getConfigurationByName(propertyName);
            if (config == null)
            {
                return defaultValue;
            }
            else {
                return config.Value;
            }
        }

        public int getPropertyAsInt(string propertyName) {
            return getPropertyAsInt(propertyName, 0);
        }

        public int getPropertyAsInt(string propertyName, int defaultValue) {
            int val = -1;
            string propVal = this.getProperty(propertyName, defaultValue.ToString());
            int.TryParse(propVal,out val);
            return val;
        }

        public bool getPropertyAsBoolean(string propertyName) {
            return getPropertyAsBoolean(propertyName, false);
        }

        public bool getPropertyAsBoolean(string propertyName, bool defaultValue)
        {
            bool val = false;
            string propVal = this.getProperty(propertyName, defaultValue.ToString());
            bool.TryParse(propVal, out val);
            return val;
        }

    }
}