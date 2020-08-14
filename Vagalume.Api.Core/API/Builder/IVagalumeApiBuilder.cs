using System.Net.Http;

namespace Vagalume.Api.Core.API.Builder
{
    public interface IVagalumeApiBuilder
    {
        IVagalumeApi Build();
        IVagalumeApiBuilder UseHttpClient(HttpClient httpClient);
        IVagalumeApiBuilder UseHttpClientHandler(HttpClientHandler handler);
    }
}
