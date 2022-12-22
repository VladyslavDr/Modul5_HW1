using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul5Task.Services.Abstractions;
using Newtonsoft.Json;

namespace Modul5Task.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest content = null)
            where TRequest : class
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Method = method;

            if (content != null)
            {
                httpRequestMessage.Content =
                    new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var resultRequest = await client.SendAsync(httpRequestMessage);

            if (resultRequest.IsSuccessStatusCode)
            {
                var resultRequestContent = await resultRequest.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(resultRequestContent);
                return response!;
            }

            return default(TResponse)!;
        }
    }
}
