using iotc_csharp_service.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            HttpResponseMessage responseMsg = null;
            try
            {
                GetHeaders();
                responseMsg = await client.GetAsync(path);
            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
            await HandleError(responseMsg);
            return await HandleSuccess(responseMsg);
        }

        public async Task<string> Post(string path, string data)
        {
            HttpResponseMessage responseMsg;
            try
            {
                GetHeaders();
                responseMsg = await client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
            await HandleError(responseMsg);
            return await HandleSuccess(responseMsg);
        }

        public async Task<string> Put(string path, string data)
        {
            HttpResponseMessage responseMsg;
            try
            {
                GetHeaders();
                responseMsg = await client.PutAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));

            }
            catch (Exception e)
            {
                throw new DataException("I/O exception occured", IOTCENTRAL_DATA_EXCEPTION_CODES.IOEXCEPTION);
            }
            await HandleError(responseMsg);
            return await HandleSuccess(responseMsg);
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

        private async Task HandleError(HttpResponseMessage msg)
        {
            HttpStatusCode code = msg.StatusCode;
            if (code != HttpStatusCode.OK && code != HttpStatusCode.Created)
            {
                string content = await msg.Content.ReadAsStringAsync();
                if (code == HttpStatusCode.Unauthorized)
                {
                    try
                    {
                        var erroObj = JObject.Parse(content);

                        throw new AuthenticationException(erroObj["error"]["code"].Value<string>());
                    }
                    catch (JsonReaderException readex)
                    {
                        throw new Exception("Unknown error", readex);
                    }
                }

                //throw new DataException(msg.ReasonPhrase + ":" + content, (int)code);

            }
        }
    }
}
