﻿#region License, terms and author information
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
using System.Web.Mvc;

namespace ElmahSensitiveDataFiltering
{
    public class ElmahSensitiveDataAttribute : ActionFilterAttribute
    {

        private readonly IEnumerable<string> _sensitiveFormDataNames;

        public ElmahSensitiveDataAttribute(params string[] sensitiveFormDataNames)
        {
            _sensitiveFormDataNames = sensitiveFormDataNames.Select(s => s.ToLowerInvariant());
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ElmahSensitiveDataFilterEventHandler.AttachEvent();
            filterContext.HttpContext.Items.Add(Constants.HttpContextItemsKey, _sensitiveFormDataNames);
        }
    }
}
