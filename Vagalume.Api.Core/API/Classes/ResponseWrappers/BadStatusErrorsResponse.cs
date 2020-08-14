using Newtonsoft.Json;
using Vagalume.Api.Core.API.Classes.ResponseWrappers.BaseResponse;

namespace Vagalume.Api.Core.API.Classes.ResponseWrappers
{
    public class BadStatusErrorsResponse : BaseStatusResponse
    {
        [JsonProperty("message")] public MessageErrorsResponse Message { get; set; }
    }
}
