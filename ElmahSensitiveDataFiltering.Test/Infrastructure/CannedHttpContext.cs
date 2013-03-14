using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

namespace ElmahSensitiveDataFiltering.Test.Infrastructure
{
    public class CannedHttpContext : HttpContextBase
    {
        private readonly IDictionary _items = new Dictionary<object, object>();
        private readonly HttpRequestBase _request;

        public CannedHttpContext(Dictionary<string, string> formValues = null)
        {
            _request = new CannedHttpRequest(formValues);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }

        public override IDictionary Items 
        {
            get { return _items; }
        }
    }
}
