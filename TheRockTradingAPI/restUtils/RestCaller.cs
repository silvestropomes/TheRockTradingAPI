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

        public RestCaller(ApiConfig apiConfig)
        {
            this.httpClient = new HttpClient();
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
            var message =  new HttpRequestMessage
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
            var nonce = DateTime.Now.Ticks / 1000L;
            var signature = hashCalculator.ComputeHash(Encoding.UTF8.GetBytes($"{nonce}{message.RequestUri.AbsoluteUri}"));
            var b16Signature = BitConverter.ToString(signature).Replace("-", "").ToLower();

            message.Headers.Add("X-TRT-KEY", this.apiConfig.ApiKey);
            message.Headers.Add("X-TRT-SIGN", b16Signature);
            message.Headers.Add("X-TRT-NONCE", nonce.ToString());

            return message;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
