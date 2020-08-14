using System;
using System.Net.Http;
using Vagalume.Api.Core.API.Classes;

namespace Vagalume.Api.Core.API.Builder
{
    public class VagalumeApiBuilder : IVagalumeApiBuilder
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private IHttpRequestProcessor _httpRequestProcessor;

        private VagalumeApiBuilder()
        {
        }

        public IVagalumeApi Build()
        {
            if (_httpClient == null)
                _httpClient = new HttpClient(_httpHandler) { BaseAddress = new Uri(VagalumeApiConstants.VAGALUME_URL) };

            if (_httpRequestProcessor == null)
                _httpRequestProcessor = new HttpRequestProcessor(_httpClient, _httpHandler);

            var vagalumeApi = new VagalumeApi(_httpRequestProcessor);
            return vagalumeApi;
        }

        public static IVagalumeApiBuilder CreateBuilder()
        {
            return new VagalumeApiBuilder();
        }
        
        public IVagalumeApiBuilder UseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        public IVagalumeApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            _httpHandler = handler;
            return this;
        }
    }
}
