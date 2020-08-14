using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vagalume.Api.Domain
{
    public class BuscaDataWrapper
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("numFound")]
        public int NumFound { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("docs")]
        public List<DocBusca> Docs { get; set; }
    }

    public class DocBusca
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("langID")]
        public int LangID { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("band")]
        public string Band { get; set; }

        [JsonProperty("fmRadios")]
        public List<string> FmRadios { get; set; }
    }
}
