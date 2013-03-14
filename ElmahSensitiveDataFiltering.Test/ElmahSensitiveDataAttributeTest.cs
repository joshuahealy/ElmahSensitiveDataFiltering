using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using ElmahSensitiveDataFiltering.Test.Infrastructure;
using NUnit.Framework;

namespace ElmahSensitiveDataFiltering.Test
{
    [TestFixture]
    public class ElmahSensitiveDataAttributeTest
    {
        private const string HttpContextItemsKey = "ElmahSensitiveDataFiltering.SensitiveFormDataNames";

        [Test]
        public void ShouldAddFormDataNamesToHttpContext()
        {
            var sensitiveFormDataNames = new[] {"password", "creditcard"};
            var elmahSensitiveDataAttribute = new ElmahSensitiveDataAttribute(sensitiveFormDataNames);
            var actionExecutingContext = new ActionExecutingContext(new ControllerContext(new CannedHttpContext(), new RouteData(), new CannedController()), new CannedActionDescriptor(), new Dictionary<string, object>());
            elmahSensitiveDataAttribute.OnActionExecuting(actionExecutingContext);

            Assert.That(actionExecutingContext.HttpContext.Items.Contains(HttpContextItemsKey));
            Assert.That(actionExecutingContext.HttpContext.Items[HttpContextItemsKey], Is.EqualTo(sensitiveFormDataNames));
        }
    }
}
