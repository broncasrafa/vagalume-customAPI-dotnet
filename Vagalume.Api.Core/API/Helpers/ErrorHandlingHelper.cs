﻿using System;
using Newtonsoft.Json;
using Vagalume.Api.Core.API.Classes.ResponseWrappers;

namespace Vagalume.Api.Core.API.Helpers
{
    internal static class ErrorHandlingHelper
    {
        internal static BadStatusResponse GetBadStatusFromJsonString(string json)
        {
            var badStatus = new BadStatusResponse();
            try
            {
                if (json == "Oops, an error occurred\n")
                    badStatus.Message = json;
                else badStatus = JsonConvert.DeserializeObject<BadStatusResponse>(json);
            }
            catch (Exception ex)
            {
                badStatus.Message = ex.Message;
            }

            return badStatus;
        }
    }
}
