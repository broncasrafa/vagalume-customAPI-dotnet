using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Vagalume.Api.Core.API.Helpers
{
    public class HttpHelper
    {
        public static HttpRequestMessage GetDefaultRequest(HttpMethod method, Uri uri)
        {
            var request = new HttpRequestMessage(method, uri);
            request.Headers.Add("Accept", HttpRequestConstants.ACCEPT);
            request.Headers.Add("Accept-Encoding", HttpRequestConstants.ACCEPT_ENCODING);
            request.Headers.Add("Accept-Language", HttpRequestConstants.ACCEPT_LANGUAGE);
            request.Headers.Add("User-Agent", HttpRequestConstants.USER_AGENT);            
            return request;
        }
        public static string GetRequestString(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add(HttpRequestHeader.UserAgent, HttpRequestConstants.USER_AGENT);
            request.Headers.Add(HttpRequestHeader.Accept, HttpRequestConstants.ACCEPT);
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, HttpRequestConstants.ACCEPT_LANGUAGE);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, HttpRequestConstants.ACCEPT_ENCODING);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public static HttpWebRequest GetDefaultRequest(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add(HttpRequestHeader.UserAgent, HttpRequestConstants.USER_AGENT);
            request.Headers.Add(HttpRequestHeader.Accept, HttpRequestConstants.ACCEPT);
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, HttpRequestConstants.ACCEPT_LANGUAGE);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, HttpRequestConstants.ACCEPT_ENCODING);
            return request;
        }
        public static HttpWebResponse GetDefaultResponse(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }

    internal class HttpRequestConstants
    {
        public const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36";
        public const string ACCEPT = "*/*";
        public const string ACCEPT_ENCODING = "gzip, deflate, br";
        public const string ACCEPT_LANGUAGE = "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7";
    }
}
