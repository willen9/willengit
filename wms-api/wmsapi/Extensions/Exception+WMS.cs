using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace wmsapi
{
    public static class ExceptionExtension
    {
        public static string ToJSONMessage(this Exception exception)
        {
            return ToJSONMessage(exception, 9999);
        }

        public static string ToJSONMessage(this Exception exception, int code)
        {
            return APIUtility.GetErrorResponseJSONMessage(code, exception);
        }
    }
}