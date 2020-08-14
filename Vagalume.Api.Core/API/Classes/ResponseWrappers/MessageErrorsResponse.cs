using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vagalume.Api.Core.API.Classes.ResponseWrappers
{
    public class MessageErrorsResponse
    {
        [JsonProperty("errors")] public List<string> Errors { get; set; }
    }
}
