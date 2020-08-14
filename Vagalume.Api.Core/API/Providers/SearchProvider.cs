using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Core.API.Helpers;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public class SearchProvider : ISearchProvider
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public SearchProvider(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }

        public async Task<IResult<List<DocBusca>>> PesquisarArtistasAsync(string q, int limit = 10)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_BUSCAR_ARTISTA_URL, q, limit);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<List<DocBusca>>(response, json);

                var result = JsonConvert.DeserializeObject<BuscaDataWrapper>(json);
                return Result.Success(result.Response.Docs);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<DocBusca>>(ex.Message);
            }
        }

        public async Task<IResult<List<DocBusca>>> PesquisarMusicasArtistas(string q, int limit = 10)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_BUSCAR_MUSICAS_ARTISTA_URL, q, limit);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<List<DocBusca>>(response, json);

                var result = JsonConvert.DeserializeObject<BuscaDataWrapper>(json);
                return Result.Success(result.Response.Docs);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<DocBusca>>(ex.Message);
            }
        }

        public async Task<IResult<List<DocBusca>>> PesquisarTrechoMusica(string q, int limit = 10)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_BUSCAR_TRECHO_MUSICA_URL, q, limit);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<List<DocBusca>>(response, json);

                var result = JsonConvert.DeserializeObject<BuscaDataWrapper>(json);
                return Result.Success(result.Response.Docs);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<DocBusca>>(ex.Message);
            }
        }
    }
}
