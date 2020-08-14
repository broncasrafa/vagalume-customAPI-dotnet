using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vagalume.Api.Domain
{
    public class Album
    {
        public string Art { get; set; }
        public string Alb { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Label { get; set; }
        public string Published { get; set; }
        public string ImgCover { get; set; }
        public int TrackCount { get; set; }
        public FotoAlbum FotoAlbum { get; set; }
        public List<Musica> Musicas { get; set; }
    }

    public class Alb
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }
    }

    public class FotoAlbum
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