#region License, terms and author information
// Elmah Sensitive Data Filtering
//
// Copyright 2013 Joshua Healy.  All rights reserved.
//
// Author(s):
//
//    Joshua Healy
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;

namespace ElmahSensitiveDataFiltering
{
    internal static class ElmahSensitiveDataFilterEventHandler
    {
        private static bool _eventAttached = false;
        private static readonly object _locker = new object();

        internal static void AttachEvent()
        {
            if (!_eventAttached && HttpContext.Current != null)
            {
                lock (_locker)
                {
                    var errorModule = GetErrorLogModule(HttpContext.Current.ApplicationInstance);
                    if (errorModule != null)
                    {
                        errorModule.Filtering += FilterException;
                        _eventAttached = true;
                    }
                }
            }
        }

        private static void FilterException(object sender, ExceptionFilterEventArgs args)
        {
            var httpContext = args.Context as HttpContext;
            var error = new Error(args.Exception, httpContext.ApplicationInstance.Context);

            var sensitiveFormDataNames = httpContext.Items[Constants.HttpContextItemsKey] as IEnumerable<string>;
            if (sensitiveFormDataNames == null)
            {
                return;
            }

            var sensitiveFormData = httpContext.Request.Form.AllKeys
                .Where(key => sensitiveFormDataNames.Contains(key.ToLowerInvariant()))
                .ToList();
            if (sensitiveFormData.Any())
            {
                sensitiveFormData.ForEach(k => error.Form.Set(k, Config.SensitiveDataReplacementText));
                ErrorLog.GetDefault(httpContext).Log(error);
                args.Dismiss();
            }
        }

        private static ErrorLogModule GetErrorLogModule(HttpApplication application)
        {
            foreach (string key in application.Modules)
            {
                if (application.Modules[key] is ErrorLogModule)
                {
                    return (application.Modules[key] as ErrorLogModule);
                }
            }
            return null;
        }
    }
}
