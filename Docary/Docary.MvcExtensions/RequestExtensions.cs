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

            var urlContainsMobile = absoluteUri.IndexOf("mobile", StringComparison.OrdinalIgnoreCase) > -1;
            if (urlContainsMobile)
                return mobileArea;

            var urlContainsDesktop = absoluteUri.IndexOf("desktop", StringComparison.OrdinalIgnoreCase) > -1;
            if (urlContainsDesktop)
                return desktopArea;

            return isMobileDevice ? mobileArea : desktopArea;
        }
    }
}
