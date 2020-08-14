using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vagalume.Api.Domain
{
    public class Musica
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lang")]
        public int Lang { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translate")]
        public List<Translate> Translate { get; set; }

        [JsonProperty("alb")]
        public Alb Alb { get; set; }
    }
}
