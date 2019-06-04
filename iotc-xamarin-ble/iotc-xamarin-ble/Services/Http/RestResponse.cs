using System;
using System.Collections.Generic;
using System.Text;

namespace iotc_xamarin_ble.Services.Http
{
    public class RestResponse
    {
        public RestResponse(bool success, string responseBody, int statusCode)
        {
            Success = success;
            ResponseBody = responseBody;
            StatusCode = statusCode;
        }

        public bool Success { get; set; }
        public string ResponseBody { get; set; }
        public int StatusCode { get; set; }
    }
}
