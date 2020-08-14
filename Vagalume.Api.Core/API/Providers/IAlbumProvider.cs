using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public interface IAlbumProvider
    {
        Task<IResult<List<TopAlbums>>> GetTop100Albums(string tipo, int mes, int ano);
        Task<IResult<Album>> GetAlbum(string artId, string albId);
    }
}
