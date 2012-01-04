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
            return request.Browser.IsMobileDevice ? "Mobile" : "Desktop";
        }
    }
}
