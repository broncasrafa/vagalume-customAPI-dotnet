using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Core.API.Helpers;
using Vagalume.Api.Domain;
using HtmlAgilityPack;

namespace Vagalume.Api.Core.API.Providers
{
    public class AlbumProvider : IAlbumProvider
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public AlbumProvider(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }

        public async Task<IResult<Album>> GetAlbum(string artId, string albId)
        {
            try
            {
                var url = string.Format(VagalumeApiConstants.ALBUM_URL, artId, albId);
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var htmlBody = htmlDoc.GetElementbyId("body");

                HtmlNode letrasWrapperElem = htmlBody.Descendants("div").Where(c => c.GetAttributeValue("class", "").Contains("letrasWrapper")).FirstOrDefault();
                List<HtmlNode> topLetrasWrapperList = letrasWrapperElem.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("topLetrasWrapper")).ToList();

                Album album = new Album();

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
                        foreach (var li in olElem.ChildNodes)
                        {
                            HtmlNode lineColLeftElem = li.Descendants("div").Where(c => c.GetAttributeValue("class", "").Equals("lineColLeft")).FirstOrDefault();
                            var linkAlbum = lineColLeftElem.ChildNodes.Where(c => c.Name == "a").FirstOrDefault()?.Attributes.Where(attr => attr.Name == "href").FirstOrDefault()?.Value?.Trim();
                            var nameMus = lineColLeftElem.ChildNodes.Where(c => c.Name == "a").FirstOrDefault()?.InnerText?.Trim();
                            var id = String.IsNullOrEmpty(linkAlbum) ? null : linkAlbum.Split('/')[2].Trim().Replace(".html", "");

                            Musica musica = new Musica() { Id = id, Url = linkAlbum, Name = nameMus };
                            Musicas.Add(musica);
                        }
                    }

                    album.Name = namealbum;
                    album.Url = link;
                    album.ImgCover = img ?? webp;
                    album.FotoAlbum = new FotoAlbum { Url = img, Webp = webp };
                    album.Art = artId;
                    album.Label = record;
                    album.Published = year;
                    album.Alb = link.Split('/')[3].Replace(".html", "");
                    album.TrackCount = tracksCount;
                    album.Musicas = Musicas;
                }

                return Result.Success(album);
            }
            catch (Exception ex)
            {
                return Result.Fail<Album>(ex.Message);
            }
        }

        public async Task<IResult<List<TopAlbums>>> GetTop100Albums(string tipo, int mes, int ano)
        {
            try
            {
                var url = $"{VagalumeApiConstants.TOP100_ALBUMS_URL}{ProviderHelper.TratarParametrosTop100(tipo, mes, ano)}";
                var html = await ProviderHelper.GetDefaultHtmlText(url, _httpRequestProcessor);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                List<TopAlbums> result = new List<TopAlbums>();

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
                    var artistname = cardCenterCol.Descendants("p").Where(c => c.GetAttributeValue("class", "").Equals("styleBlack")).FirstOrDefault()?.InnerText?.Trim();

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

                    TopAlbums TopAlbums = new TopAlbums()
                    {
                        Position = position,
                        Name = name,
                        Url = link,
                        Art = link.Split('/')[1],
                        Alb = link.Split('/')[3].Replace(".html", ""),
                        ArtistName = artistname,
                        FotoAlbum = new FotoAlbum { Webp = webp, Url = img }
                    };

                    result.Add(TopAlbums);
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<TopAlbums>>(ex.Message);
            }
        }
    }
}
