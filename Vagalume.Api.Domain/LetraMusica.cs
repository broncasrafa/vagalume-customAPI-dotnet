using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vagalume.Api.Domain
{
    public class LetraMusica
    {
        [JsonProperty("art")]
        public Artista Artista { get; set; }

        [JsonProperty("mus")]
        public List<Musica> Musicas { get; set; }
    }

    public class Translate
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lang")]
        public int Lang { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
