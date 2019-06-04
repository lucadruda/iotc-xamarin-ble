using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iotc_xamarin_ble.Services.Http
{
    public class RestClient : IHttpClient
    {
        private HttpClient client;

        public RestClient()
        {
            client = new HttpClient();
        }

        public async Task<RestResponse> Get(string path, string token)
        {
            return await Get(path, token, null);
        }

        public async Task<RestResponse> Get(string path, string token, Dictionary<string, string> headers)
        {
            GetHeaders(token, headers);
            HttpResponseMessage msg = await client.GetAsync(path);
            return new RestResponse(msg.IsSuccessStatusCode, await msg.Content.ReadAsStringAsync(), (int)msg.StatusCode);
        }


        public async Task<RestResponse> Post(string path, string token, string body, Dictionary<string, string> headers)
        {
            return await Post(path, token, body, "application/json", headers);
        }

        public async Task<RestResponse> Post(string path, string token, string body)
        {
            return await Post(path, token, body, "application/json", null);

        }

        public async Task<RestResponse> Post(string path, string token, string body, string contentType)
        {
            return await Post(path, token, body, contentType, null);

        }

        public async Task<RestResponse> Post(string path, string token, string body, string contentType, Dictionary<string, string> headers)
        {
            GetHeaders(token, headers);
            HttpResponseMessage msg = await client.PostAsync(path, new StringContent(body, Encoding.UTF8, contentType));
            return new RestResponse(msg.IsSuccessStatusCode, await msg.Content.ReadAsStringAsync(), (int)msg.StatusCode);
        }

        public async Task<RestResponse> Put(string path, string token, string body, Dictionary<string, string> headers)
        {
            return await Put(path, token, body, "application/json", headers);
        }

        public async Task<RestResponse> Put(string path, string token, string body)
        {
            return await Put(path, token, body, "application/json", null);

        }

        public async Task<RestResponse> Put(string path, string token, string body, string contentType)
        {
            return await Put(path, token, body, contentType, null);
        }

        public async Task<RestResponse> Put(string path, string token, string body, string contentType, Dictionary<string, string> headers)
        {
            GetHeaders(token, headers);
            HttpResponseMessage msg = await client.PutAsync(path, new StringContent(body, Encoding.UTF8, contentType));
            return new RestResponse(msg.IsSuccessStatusCode, await msg.Content.ReadAsStringAsync(), (int)msg.StatusCode);
        }

        private void GetHeaders(string token, Dictionary<string, string> headers = null)
        {
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                }
            }
        }
    }
}
