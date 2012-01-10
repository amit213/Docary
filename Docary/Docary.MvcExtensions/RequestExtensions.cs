using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Docary.MvcExtensions
{
    public static class RequestExtensions
    {
        public static string ResolveArea(this HttpRequestBase request)
        {
            return ResolveAreaInternal(request.Url.AbsoluteUri, request.Browser.IsMobileDevice);
        }

        public static string ResolveArea(this HttpRequest request)
        {
            return ResolveAreaInternal(request.Url.AbsoluteUri, request.Browser.IsMobileDevice);
        }

        private static string ResolveAreaInternal(string absoluteUri, bool isMobileDevice)
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
