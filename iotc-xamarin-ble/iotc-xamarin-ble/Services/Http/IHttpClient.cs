using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services.Http
{
    public interface IHttpClient
    {

        Task<RestResponse> Get(string path, string token);
        Task<RestResponse> Get(string path, string token, Dictionary<string, string> headers);
        Task<RestResponse> Put(string path, string token, string body, Dictionary<string, string> headers);
        Task<RestResponse> Post(string path, string token, string body, Dictionary<string, string> headers);

        Task<RestResponse> Put(string path, string token, string body);
        Task<RestResponse> Post(string path, string token, string body);

        Task<RestResponse> Put(string path, string token, string body, string contentType);
        Task<RestResponse> Post(string path, string token, string body, string contentType);

        Task<RestResponse> Put(string path, string token, string body, string contentType, Dictionary<string, string> headers);
        Task<RestResponse> Post(string path, string token, string body, string contentType, Dictionary<string, string> headers);

    }
}
