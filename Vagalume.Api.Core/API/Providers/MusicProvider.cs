using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Core.API.Helpers;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public class MusicProvider : IMusicProvider
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        
        public MusicProvider(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }



        public async Task<IResult<LetraMusica>> GetLetraMusica(string artista, string musica)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_LETRA_MUSICA_URL, artista, musica);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<LetraMusica>(response, json);

                var result = JsonConvert.DeserializeObject<LetraMusica>(json);
                return Result.Success(result);
            }
            catch(Exception ex)
            {
                return Result.Fail<LetraMusica>(ex.Message);
            }
        }

        public async Task<IResult<List<TopMusics>>> GetTop100Musicas(string tipo, int mes, int ano)
        {
            try
            {
                var url = $"{VagalumeApiConstants.TOP100_MUSICAS_URL}{ProviderHelper.TratarParametrosTop100(tipo, mes, ano)}";
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                List<TopMusics> result = new List<TopMusics>();

                var htmlBody = htmlDoc.GetElementbyId("top100Page");
                //var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//div");

                var olElem = htmlBody.Descendants("ol").Where(c => c.GetAttributeValue("class", "").Equals("topCard")).ToList();
                foreach (HtmlNode li in olElem[0].ChildNodes)
                {
                    HtmlNode topPosition = li.ChildNodes.Where(c => c.Name == "div").FirstOrDefault();
                    var position = topPosition.ChildNodes.Where(c => c.Name == "p").FirstOrDefault()?.InnerText?.ToString()?.Trim();

                    HtmlNode aElem = li.Descendants("a").Where(c => c.GetAttributeValue("class", "").Contains("w1")).FirstOrDefault();
                    var link = aElem.Attributes.Where(c => c.Name == "href").FirstOrDefault()?.Value?.Replace(".html", "")?.Trim();
                    var name = aElem.InnerText?.Trim();

                    HtmlNode pElem = li.Descendants("p").Where(c => c.GetAttributeValue("class", "").Contains("styleBlack")).FirstOrDefault();
                    var artistname = pElem.InnerText?.Trim();

                    TopMusics TopMusics = new TopMusics()
                    {
                        Position = position,
                        Name = name,
                        ArtistName = artistname,
                        Url = link,
                        Art = link.Split('/')[1],
                        Mus = link.Split('/')[2]
                    };

                    result.Add(TopMusics);
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<TopMusics>>(ex.Message);
            }
        }
    }
}
