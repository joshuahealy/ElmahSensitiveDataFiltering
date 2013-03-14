using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ElmahSensitiveDataFiltering.Test.Infrastructure
{
    public class CannedActionDescriptor : ActionDescriptor
    {
        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
        {
            return new object();
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return new ParameterDescriptor[0];
        }

        public override string ActionName
        {
            get { return String.Empty; }
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
