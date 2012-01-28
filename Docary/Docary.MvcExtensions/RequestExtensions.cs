using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Docary.MvcExtensions
{
    public static class RequestExtensions
    {
        public static string ResolveDestinationArea(this HttpRequestBase request)
        {
            return ResolveDestinationAreaInternal(request.Url.AbsoluteUri, request.Browser.IsMobileDevice);
        }

        public static string ResolveDestinationArea(this HttpRequest request)
        {
            return ResolveDestinationAreaInternal(request.Url.AbsoluteUri, request.Browser.IsMobileDevice);
        }

        private static string ResolveDestinationAreaInternal(string absoluteUri, bool isMobileDevice)
        {
            var mobileArea = "mobile";
            var desktopArea = "desktop";

            var uri = new Uri(absoluteUri);
            var valueToSearchIn = uri.GetLeftPart(UriPartial.Path);
            
            var returnUrl = HttpUtility.ParseQueryString(uri.Query).Get("returnUrl");
            if (!string.IsNullOrEmpty(returnUrl))
                valueToSearchIn = returnUrl;

            var urlContainsMobile = valueToSearchIn.IndexOf("mobile", StringComparison.OrdinalIgnoreCase) > -1;
            if (urlContainsMobile)
                return mobileArea;

            var urlContainsDesktop = valueToSearchIn.IndexOf("desktop", StringComparison.OrdinalIgnoreCase) > -1;
            if (urlContainsDesktop)
                return desktopArea;

            return isMobileDevice ? mobileArea : desktopArea;
        }
    }
}
