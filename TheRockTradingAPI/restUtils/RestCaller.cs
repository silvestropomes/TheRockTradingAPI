using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheRockTradingAPI.contract;

namespace TheRockTradingAPI.restUtils
{
    internal class RestCaller : IRestCaller, IDisposable
    {
        private HttpClient httpClient;
        private readonly ApiConfig apiConfig;
        private readonly HMACSHA512 hashCalculator;

        public RestCaller(ApiConfig apiConfig, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiConfig = apiConfig;
            if (this.apiConfig.AreKeysPresent)
            {
                this.hashCalculator = new HMACSHA512(Encoding.UTF8.GetBytes(apiConfig.PrivateKey));
            }
        }

        public async Task<T> GetAsync<T>(string uri) where T : IResponse
        {
            var message = GetStandardRequestMessage(HttpMethod.Get, uri);
            if (this.apiConfig.AreKeysPresent)
            {
                message = GetAuthenticatedRequestMessage(message);
            }
            var response = await httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();


            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public T Get<T>(string uri) where T : IResponse
        {
            return GetAsync<T>(uri).Result;
        }

        private HttpRequestMessage GetStandardRequestMessage(HttpMethod httpMethod, string uri)
        {
            var message = new HttpRequestMessage
            {
                Method = httpMethod,
                Content = new StringContent(""),
                RequestUri = new Uri(uri)
            };
            message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return message;
        }

        private HttpRequestMessage GetAuthenticatedRequestMessage(HttpRequestMessage message)
        {

            var headers = GetAuthenticationHeaders(message);
            foreach(var header in headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }

        private IList<KeyValuePair<string, string>> GetAuthenticationHeaders(HttpRequestMessage message)
        {
            var nonce = DateTime.Now.Ticks / 1000L;
            var signature = hashCalculator.ComputeHash(Encoding.UTF8.GetBytes($"{nonce}{message.RequestUri.AbsoluteUri}"));
            var b16Signature = BitConverter.ToString(signature).Replace("-", "").ToLower();

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("X-TRT-KEY", this.apiConfig.ApiKey));
            headers.Add(new KeyValuePair<string, string>("X-TRT-SIGN", b16Signature));
            headers.Add(new KeyValuePair<string, string>("X-TRT-NONCE", nonce.ToString()));

            return headers;
        }

        public async Task<T> PostAsync<T>(string uri, string postContent) where T : IResponse
        {
            var message = GetStandardRequestMessage(HttpMethod.Post, uri);

            StringContent content = new StringContent(postContent, Encoding.UTF8, "application/json");

            if (this.apiConfig.AreKeysPresent)
            {
                message = GetAuthenticatedRequestMessage(message);
            }
            message.Content = content;

            var response = await httpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public T Post<T>(string uri, string postContent) where T : IResponse
        {
            return this.PostAsync<T>(uri, postContent).Result;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
