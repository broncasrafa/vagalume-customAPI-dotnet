using System;

namespace Vagalume.Api.Core.API.Classes
{
    public class ResultInfo
    {
        public Exception Exception { get; }
        public string Message { get; }
        public ResponseType ResponseType { get; }

        public ResultInfo(string message)
        {
            Message = message;
        }

        public ResultInfo(Exception exception)
        {
            Exception = exception;
            Message = exception?.Message;
            ResponseType = ResponseType.InternalException;
        }

        public ResultInfo(ResponseType responseType, string errorMessage)
        {
            ResponseType = responseType;
            Message = errorMessage;
        }        

        public override string ToString()
        {
            return $"{ResponseType.ToString()}: {Message}.";
        }
    }
}
