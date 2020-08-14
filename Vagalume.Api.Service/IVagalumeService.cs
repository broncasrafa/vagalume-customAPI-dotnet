using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Service
{
    public interface IVagalumeService
    {
        #region [ Album ]
        Task<IResult<List<TopAlbums>>> GetTop100Albums(string tipo, int mes, int ano);
        Task<IResult<Album>> GetAlbum(string artId, string albId);
        #endregion

        #region [ Artist ]
        Task<IResult<List<TopArtists>>> GetTop100Artistas(string tipo, int mes, int ano);
        Task<IResult<Discografia>> GetDiscografiaArtista(string artId);
        Task<IResult<Biografia>> GetBiografiaArtista(string artId);
        Task<IResult<List<FotoArtista>>> GetFotosArtista(string artId);
        Task<IResult<Artista>> GetArtista(string art);
        #endregion

        #region [ Music ]
        Task<IResult<List<TopMusics>>> GetTop100Musicas(string tipo, int mes, int ano);
        #endregion

        #region [ Search ]
        #endregion
    }
}
