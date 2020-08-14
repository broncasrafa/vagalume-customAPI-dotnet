using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vagalume.Api.Domain
{
    public class ArtistaDataWrapper
    {
        [JsonProperty("artist")]
        public Artista Artista { get; set; }
    }
    public class FotosArtistaWrapper
    {
        [JsonProperty("images")]
        public List<FotoArtista> Images { get; set; }
    }

    public class Artista
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("pic_small")]
        public string PicSmall { get; set; }

        [JsonProperty("pic_medium")]
        public string PicMedium { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("genre")]
        public List<Genero> Genero { get; set; }

        [JsonProperty("related")]
        public List<Relacionada> Relacionadas { get; set; }

        [JsonProperty("toplyrics")]
        public Toplyrics Toplyrics { get; set; }

        [JsonProperty("lyrics")]
        public Lyrics Lyrics { get; set; }

        [JsonProperty("albums")]
        public Albums Albums { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("turl")]
        public string Turl { get; set; }
    }

    public class Albums
    {
        [JsonProperty("item")]
        public List<Item> Item { get; set; }
    }

    public class Lyrics
    {
        [JsonProperty("item")]
        public List<Item> Item { get; set; }
    }

    public class Toplyrics
    {

        [JsonProperty("item")]
        public List<Item> Item { get; set; }
    }

    public class Relacionada
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Rank
    {

        [JsonProperty("pos")]
        public string Pos { get; set; }

        [JsonProperty("period")]
        public int Period { get; set; }

        [JsonProperty("views")]
        public string Views { get; set; }

        [JsonProperty("uniques")]
        public string Uniques { get; set; }

        [JsonProperty("points")]
        public string Points { get; set; }
    }

    public class Genero
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Biografia
    {
        public string Art { get; set; }
        public string Name { get; set; }
        public string Born { get; set; }
        public string Country { get; set; }
        public string Bio { get; set; }
        public string SiteOficial { get; set; }
        public string[] RedesSociais { get; set; }
        public List<FasSite> FasSites { get; set; }
    }

    public class Discografia
    {
        public string Art { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }

    public class FasSite
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class FotoArtista
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("thumbUrl")]
        public string ThumbUrl { get; set; }

        [JsonProperty("thumbWidth")]
        public string ThumbWidth { get; set; }

        [JsonProperty("thumbHeight")]
        public string ThumbHeight { get; set; }

        public string Webp { get; set; }
    }
}
