using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vagalume.Api.Core.API.Classes
{
    public class HttpRequestProcessor : IHttpRequestProcessor
    {
        public HttpClientHandler HttpHandler { get; }
        public HttpClient Client { get; }

        public HttpRequestProcessor(HttpClient httpClient, HttpClientHandler httpHandler)
        {            
            Client = httpClient;
            HttpHandler = httpHandler;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            var response = await Client.SendAsync(requestMessage);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            var response = await Client.GetAsync(requestUri);
            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
        {
            var response = await Client.SendAsync(requestMessage, completionOption);
            return response;
        }

        public async Task<string> SendAndGetJsonAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption)
        {
            var response = await Client.SendAsync(requestMessage, completionOption);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GeJsonAsync(Uri requestUri)
        {
            var response = await Client.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
