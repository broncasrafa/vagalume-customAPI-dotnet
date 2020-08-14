using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public interface IMusicProvider
    {
        Task<IResult<List<TopMusics>>> GetTop100Musicas(string tipo, int mes, int ano);
        Task<IResult<LetraMusica>> GetLetraMusica(string artista, string musica);
    }
}
