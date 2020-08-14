using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API
{
    public interface IVagalumeApi
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
        Task<IResult<List<FotoArtista>>> GetFotosArtistaApi(string bandId, int limit = 10);
        Task<IResult<Artista>> GetArtista(string art);
        #endregion

        #region [ Music ]
        Task<IResult<List<TopMusics>>> GetTop100Musicas(string tipo, int mes, int ano);
        Task<IResult<LetraMusica>> GetLetraMusica(string artista, string musica);
        #endregion

        #region [ Search ]
        Task<IResult<List<DocBusca>>> PesquisarArtistas(string q, int limit = 10);
        Task<IResult<List<DocBusca>>> PesquisarTrechoMusica(string q, int limit = 10);
        Task<IResult<List<DocBusca>>> PesquisarMusicasArtistas(string q, int limit = 10);
        #endregion

    }
}


#region [ Album ]
#endregion

#region [ Artist ]
#endregion

#region [ Music ]
#endregion

#region [ Search ]
#endregion