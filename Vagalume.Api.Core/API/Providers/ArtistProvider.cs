using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Core.API.Helpers;
using Vagalume.Api.Domain;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Vagalume.Api.Core.API.Providers
{
    public class ArtistProvider : IArtistProvider
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public ArtistProvider(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }

        

        public async Task<IResult<List<FotoArtista>>> GetFotosArtista(string artId)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.ARTISTA_FOTOS_URL, artId);
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                List<FotoArtista> result = new List<FotoArtista>();

                var htmlBody = htmlDoc.GetElementbyId("body");

                HtmlNode gridContent = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("gridContent")).FirstOrDefault();
                List<HtmlNode> ulElems = gridContent.Descendants("ul").Where(c => c.GetAttributeValue("class", "").Contains("galleryList")).ToList();

                foreach (var ul in ulElems)
                {
                    foreach (var li in ul.ChildNodes)
                    {
                        HtmlNode pictureElem = li.ChildNodes.Where(c => c.Name == "picture").FirstOrDefault();
                        var webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "srcset").FirstOrDefault()?.Value;
                        var img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "src").FirstOrDefault()?.Value;

                        if (webp == null)
                        {
                            webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                        }
                        if (img == null)
                        {
                            img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                        }

                        FotoArtista fotoArtista = new FotoArtista()
                        {
                            Webp = webp,
                            Url = img
                        };

                        result.Add(fotoArtista);
                    }
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<FotoArtista>>(ex.Message);
            }
        }

        public async Task<IResult<List<FotoArtista>>> GetFotosArtistaApi(string bandId, int limit = 10)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_FOTOS_ARTISTA_URL, bandId, limit);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<List<FotoArtista>>(response, json);

                var result = JsonConvert.DeserializeObject<FotosArtistaWrapper>(json);
                return Result.Success(result.Images);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<FotoArtista>>(ex.Message);
            }
        }

        public async Task<IResult<List<TopArtists>>> GetTop100Artistas(string tipo, int mes, int ano)
        {
            try
            {
                var url = $"{VagalumeApiConstants.TOP100_ARTISTAS_URL}{ProviderHelper.TratarParametrosTop100(tipo, mes, ano)}";
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                List<TopArtists> result = new List<TopArtists>();

                var htmlBody = htmlDoc.GetElementbyId("top100Page");

                var olElem = htmlBody.Descendants("ol").Where(c => c.GetAttributeValue("class", "").Equals("topCard")).ToList();
                foreach (HtmlNode li in olElem[0].ChildNodes)
                {
                    HtmlNode topPosition = li.ChildNodes.Where(c => c.Name == "div").FirstOrDefault();
                    var position = topPosition.ChildNodes.Where(c => c.Name == "p").FirstOrDefault()?.InnerText?.ToString()?.Trim();

                    HtmlNode cardCenterCol = li.ChildNodes.Where(c => c.Name == "div").LastOrDefault();
                    HtmlNode aElem = cardCenterCol.ChildNodes.Where(c => c.Name == "div").FirstOrDefault().ChildNodes.Where(x => x.Name == "a").FirstOrDefault();
                    var link = aElem.Attributes.Where(c => c.Name == "href").FirstOrDefault()?.Value?.Trim();
                    var name = cardCenterCol.Descendants("a").Where(c => c.GetAttributeValue("class", "").Equals("w1 h22")).FirstOrDefault()?.InnerText?.Trim();

                    HtmlNode pictureElem = aElem.ChildNodes.Where(c => c.Name == "picture").FirstOrDefault();
                    var webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "srcset").FirstOrDefault()?.Value;
                    var img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "src").FirstOrDefault()?.Value;

                    if (webp == null)
                    {
                        webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                    }
                    if (img == null)
                    {
                        img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                    }

                    TopArtists TopArtists = new TopArtists()
                    {
                        Position = position,
                        Name = name,
                        Url = link,
                        Art = link.Split('/')[1],
                        FotoArtista = new FotoArtista { Webp = webp, Url = img }
                    };

                    result.Add(TopArtists);
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<TopArtists>>(ex.Message);
            }
        }

        public async Task<IResult<Artista>> GetArtista(string art)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.API_ARTISTA_URL, art);
                var uri = new Uri(url);
                var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
                var response = await _httpRequestProcessor.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                    return Result.UnExpectedResponse<Artista>(response, json);

                var result = JsonConvert.DeserializeObject<ArtistaDataWrapper>(json);
                return Result.Success(result.Artista);
            }
            catch (Exception ex)
            {
                return Result.Fail<Artista>(ex.Message);
            }
        }

        public async Task<IResult<Biografia>> GetBiografiaArtista(string artId)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.ARTISTA_BIOGRAFIA_URL, artId);
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                
                var htmlBody = htmlDoc.GetElementbyId("body");

                HtmlNode textCol = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("textCol")).FirstOrDefault();

                HtmlNode bioTitleBox = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("bioTitleBox")).FirstOrDefault();
                HtmlNode bioTextBox = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("bioTextBox")).FirstOrDefault();
                HtmlNode bioInfoWrapper = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("bioInfoWrapper")).FirstOrDefault();

                var name = bioTitleBox.ChildNodes.Where(c => c.Name == "h4").FirstOrDefault()?.InnerText?.Trim();
                var born = bioTitleBox.ChildNodes.Where(c => c.Name == "p").FirstOrDefault()?.InnerText?.Trim();
                var country = bioTitleBox.ChildNodes.Where(c => c.Name == "p").LastOrDefault()?.InnerText?.Trim();
                var bio = bioTextBox.ChildNodes.Where(c => c.Name == "p").FirstOrDefault()?.InnerText?.Trim().Replace("<br>", "\n").Replace("<br />", "\n");
                var siteOficial = bioInfoWrapper.Descendants("a").Where(c => c.GetAttributeValue("class", "").Equals("external-after")).FirstOrDefault()?
                                        .Attributes.Where(attr => attr.Name == "href").FirstOrDefault()?.Value?.Trim();

                List<string> socialList = new List<string>();
                var ulSocialList = bioInfoWrapper.Descendants("ul").Where(c => c.GetAttributeValue("class", "").Equals("social-list")).FirstOrDefault();
                foreach(var li in ulSocialList.ChildNodes)
                {
                    var link = li.ChildNodes.Where(c => c.Name == "a").FirstOrDefault()?.Attributes.Where(attr => attr.Name == "href").FirstOrDefault()?.Value?.Trim();
                    socialList.Add(link);
                }

                List<FasSite> fasSitesList = new List<FasSite>();
                var aElemFasSites = bioInfoWrapper.Descendants("a").Where(c => c.GetAttributeValue("class", "").Equals("external-before")).ToList();
                foreach(var a in aElemFasSites)
                {
                    var link = a.Attributes.Where(c => c.Name == "href").FirstOrDefault()?.Value?.Trim();
                    var namesite = a.InnerText?.Trim();
                    FasSite fasSite = new FasSite() { Name = namesite, Url = link };
                    fasSitesList.Add(fasSite);
                }

                Biografia result = new Biografia()
                {
                    Art = artId,
                    Name = name,
                    Born = born,
                    Country = country,
                    Bio = bio,
                    SiteOficial = siteOficial,
                    RedesSociais = socialList.ToArray(),
                    FasSites = fasSitesList
                };

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<Biografia>(ex.Message);
            }
        }

        public async Task<IResult<Discografia>> GetDiscografiaArtista(string artId)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.ARTISTA_ALBUMS_URL, artId);
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);                

                var htmlBody = htmlDoc.GetElementbyId("body");

                HtmlNode letrasWrapperElem = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Contains("letrasWrapper")).FirstOrDefault();
                List<HtmlNode> topLetrasWrapperList = letrasWrapperElem.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("topLetrasWrapper")).ToList();

                List<Album> Albums = new List<Album>();

                foreach (var item in topLetrasWrapperList)
                {
                    HtmlNode cardAlbumHeader = item.ChildNodes.Where(c => c.Name == "div" && c.Attributes.ToList().Exists(x => x.Name == "class" && x.Value == "cardAlbumHeader")).FirstOrDefault();

                    HtmlNode pictureElem = cardAlbumHeader.ChildNodes.Where(c => c.Name == "div").FirstOrDefault()?.ChildNodes.Where(x => x.Name == "picture").FirstOrDefault();
                    var webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "srcset").FirstOrDefault()?.Value;
                    var img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "src").FirstOrDefault()?.Value;

                    if (webp == null)
                    {
                        webp = pictureElem.ChildNodes.Where(c => c.Name == "source").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                    }
                    if (img == null)
                    {
                        img = pictureElem.ChildNodes.Where(c => c.Name == "img").FirstOrDefault()?.Attributes.Where(c => c.Name == "data-src").FirstOrDefault()?.Value;
                    }

                    HtmlNode cardAlbumInfosElem = cardAlbumHeader.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("cardAlbumInfos")).FirstOrDefault();
                    var year = cardAlbumInfosElem.ChildNodes.Where(c => c.Name == "p" && c.Attributes.ToList().Exists(x => x.Name == "class" && x.Value == "albumYear")).FirstOrDefault()?.InnerText?.Trim();
                    var record = cardAlbumInfosElem.ChildNodes.Where(c => c.Name == "p" && c.Attributes.ToList().Exists(x => x.Name == "class" && x.Value == "albumRecord")).FirstOrDefault()?.InnerText?.Trim();

                    HtmlNode aAlbumTitleElem = cardAlbumInfosElem.ChildNodes.Where(c => c.Name == "h3").FirstOrDefault()?.ChildNodes.Where(x => x.Name == "a").FirstOrDefault();
                    var link = aAlbumTitleElem?.Attributes.Where(attr => attr.Name == "href").FirstOrDefault()?.Value?.Trim();
                    var namealbum = aAlbumTitleElem?.InnerText?.Trim();

                    List<HtmlNode> trackWrapper = item.ChildNodes.Where(c => c.Name == "div" && c.Attributes.ToList().Exists(x => x.Name == "class" && x.Value == "trackWrapper")).ToList();
                    List<HtmlNode> olTopMusicList = item.ChildNodes.Where(c => c.Name == "ol" && c.Attributes.ToList().Exists(x => x.Name == "id" && x.Value == "topMusicList")).ToList();

                    var tracksCount = 0;
                    for (int i = 0; i < trackWrapper.Count(); i++)
                    {
                        var trackCount = trackWrapper[i].ChildNodes.Where(c => c.Name == "p").FirstOrDefault()?.InnerText?.Trim().SomenteNumeros();
                        tracksCount += Convert.ToInt32(trackCount);
                    }

                    List<Musica> Musicas = new List<Musica>();
                    for (int i = 0; i < olTopMusicList.Count(); i++)
                    {
                        var olElem = olTopMusicList[i];
                        foreach(var li in olElem.ChildNodes)
                        {
                            HtmlNode lineColLeftElem = li.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("lineColLeft")).FirstOrDefault();
                            var linkAlbum = lineColLeftElem.ChildNodes.Where(c => c.Name == "a").FirstOrDefault()?.Attributes.Where(attr => attr.Name == "href").FirstOrDefault()?.Value?.Trim();
                            var nameMus = lineColLeftElem.ChildNodes.Where(c => c.Name == "a").FirstOrDefault()?.InnerText?.Trim();
                            var id = String.IsNullOrEmpty(linkAlbum) ? null : linkAlbum.Split('/')[2].Trim().Replace(".html", "");

                            Musica musica = new Musica() { Id = id, Url = linkAlbum, Name = nameMus };
                            Musicas.Add(musica);
                        }
                    }

                    Album album = new Album()
                    {
                        Name = namealbum,
                        Url = link,
                        ImgCover = img ?? webp,
                        FotoAlbum = new FotoAlbum { Url = img, Webp = webp },
                        Art = artId,
                        Label = record,
                        Published = year,
                        Alb = link.Split('/')[3].Replace(".html", ""),
                        TrackCount = tracksCount,
                        Musicas = Musicas
                    };
                    Albums.Add(album);
                }

                var htmlTitle = htmlDoc.GetElementbyId("artHeaderTitle");
                var name = htmlTitle.Descendants("h1").Where(c => c.GetAttributeValue("class", "").Equals("darkBG")).FirstOrDefault()?.InnerText?.Trim();

                Discografia result = new Discografia()
                {
                    Art = artId,
                    Name = name,
                    Albums = Albums
                };

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<Discografia>(ex.Message);
            }
        }        
    }
}
