using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace taurus.Core.Web
{
    public class TaurusResponseMessage : HttpResponseMessage
    {
        public bool _error;
        public object _data;

        public TaurusResponseMessage(object data) : base() {
            _error = false;
            _data = data;
            init();
        }

        public TaurusResponseMessage(bool error, object data)
            : base()
        {
            _error = error;
            _data = data;
            init();
        }

        private void init() {
            this.StatusCode = System.Net.HttpStatusCode.OK;
            this.Content = new ObjectContent<object>(new { Status = (_error ? "ERROR" : "OK"), 
                jData = _data }, 
                new JsonMediaTypeFormatter());
        }


    }
}