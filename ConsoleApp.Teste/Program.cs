using Vagalume.Api.Core.API.Builder;

namespace ConsoleApp.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            var _vagalumeApi = VagalumeApiBuilder.CreateBuilder().Build();

            //var result = _vagalumeApi.GetLetraMusica("lana-del-rey", "Love");
            //var result = _vagalumeApi.GetTop100Musicas("geral", 12, 2019).GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetTop100Artistas("geral", 12, 2019).GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetTop100Albums("geral", 12, 2019).GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetFotosArtistaApi("3ade68b7g2d6e1ea3", 1000).GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetFotosArtista("lana-del-rey").GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetArtista("lana-del-rey").GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetBiografiaArtista("lana-del-rey").GetAwaiter().GetResult().Value;
            //var result = _vagalumeApi.GetDiscografiaArtista("lana-del-rey").GetAwaiter().GetResult().Value;
            var result = _vagalumeApi.GetAlbum("dua-lipa", "dua-lipa").GetAwaiter().GetResult().Value;

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
