using System;
using System.Runtime.Serialization;

namespace wmsapi
{
    public class APIException : Exception, ISerializable
    {
        public int Code { get; set; }

        public APIException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}