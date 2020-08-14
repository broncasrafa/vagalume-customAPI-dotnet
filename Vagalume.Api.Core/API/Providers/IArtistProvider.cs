using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public interface IArtistProvider
    {
        Task<IResult<List<TopArtists>>> GetTop100Artistas(string tipo, int mes, int ano);
        Task<IResult<Discografia>> GetDiscografiaArtista(string artId);
        Task<IResult<Biografia>> GetBiografiaArtista(string artId);
        Task<IResult<List<FotoArtista>>> GetFotosArtista(string artId);
        Task<IResult<List<FotoArtista>>> GetFotosArtistaApi(string bandId, int limit = 10);
        Task<IResult<Artista>> GetArtista(string art);
    }
}
