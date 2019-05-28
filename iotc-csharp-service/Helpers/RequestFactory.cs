using iotc_csharp_service.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iotc_csharp_service.Helpers
{
    public class RequestFactory
    {
        private string accessToken;
        private Dictionary<string, string> headers;
        private HttpClient client;

        public RequestFactory(string token)
        {
            accessToken = token;
            client = new HttpClient();
        }

        public async Task<string> Get(string path)
        {
            try
            {
                GetHeaders();
                HttpResponseMessage responseMsg = await client.GetAsync(path);
                HandleError(responseMsg);
                return await HandleSuccess(responseMsg);
            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
        }

        public async Task<string> Post(string path, string data)
        {
            try
            {
                GetHeaders();
                HttpResponseMessage responseMsg = await client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
                HandleError(responseMsg);
                return await HandleSuccess(responseMsg);
            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
        }

        public async Task<string> Put(string path, string data)
        {
            try
            {
                GetHeaders();
                HttpResponseMessage responseMsg = await client.PutAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
                HandleError(responseMsg);
                return await HandleSuccess(responseMsg);
            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
        }

        private void GetHeaders()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);

                }
            }
        }
        public void SetHeaders(Dictionary<string, string> headers)
        {
            this.headers = headers;
        }

        private async Task<string> HandleSuccess(HttpResponseMessage msg)
        {
            HttpStatusCode code = msg.StatusCode;
            if (code == HttpStatusCode.OK || code == HttpStatusCode.Created)
            {
                return await msg.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException("Request wasn't successfull. Code: " + code.ToString());
            }
        }

        private async void HandleError(HttpResponseMessage msg)
        {
            HttpStatusCode code = msg.StatusCode;
            if (code != HttpStatusCode.OK && code != HttpStatusCode.Created)
            {
                string content = msg.Content.ReadAsStringAsync().Result;
                throw new DataException(msg.ReasonPhrase + ":" + content, (int)code);

            }
        }
    }
}
