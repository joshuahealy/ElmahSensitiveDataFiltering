using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace ElmahSensitiveDataFiltering.Test.Infrastructure
{
    public class CannedHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection _form;

        public CannedHttpRequest(Dictionary<string,string> formValues = null)
        {
            _form = new NameValueCollection();
            if (formValues != null)
            {
                foreach (var kvp in formValues)
                {
                    _form.Add(kvp.Key, kvp.Value);
                }
            }
        }

        public override NameValueCollection Form
        {
            get
            {
                return _form;
            }
        }
    }
}
