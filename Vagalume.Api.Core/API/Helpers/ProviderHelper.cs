using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;

namespace Vagalume.Api.Core.API.Helpers
{
    public static class ProviderHelper
    {
        public static async Task<string> GetDefaultHtmlText(string url, IHttpRequestProcessor httpRequestProcessor)
        {
            var uri = new Uri(url);
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, uri);
            var response = await httpRequestProcessor.GetAsync(uri);
            var html = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return html;
        }

        public static string TratarParametrosTop100(string tipo, int? mes, int? ano)
        {
            var tipoParam = "";
            var anoParam = "";
            var mesParam = "";

            if (!String.IsNullOrEmpty(tipo))
                tipoParam += $"/{tipo}";
            else
                tipoParam += "/geral";
            
            if (ano.HasValue && ano > 2003)
                anoParam += $"/{ano}";
            else
                anoParam += $"/{DateTime.Now.Year}";

            if (mes.HasValue && (mes > 0 && mes < 13))
            {
                var mesT = mes > 9 ? mes.ToString() : $"0{mes}";
                mesParam += $"/{mesT}";
            }
            else
            {
                var currentMes = DateTime.Now.Month;
                var mesT = currentMes > 9 ? currentMes.ToString() : $"0{currentMes}";
                mesParam += $"/{mesT}";
            }

            var parametros = $"{tipoParam}{anoParam}{mesParam}";
            return parametros;
        }

        public static string SomenteNumeros(this string value)
        {
            return String.IsNullOrEmpty(value) ? null : string.Join("", value.ToCharArray().Where(Char.IsDigit));
        }
    }
}
