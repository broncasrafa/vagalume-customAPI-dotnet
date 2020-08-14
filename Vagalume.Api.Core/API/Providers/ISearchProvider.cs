using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API.Providers
{
    public interface ISearchProvider
    {
        Task<IResult<List<DocBusca>>> PesquisarArtistasAsync(string q, int limit = 10);
        Task<IResult<List<DocBusca>>> PesquisarTrechoMusica(string q, int limit = 10);
        Task<IResult<List<DocBusca>>> PesquisarMusicasArtistas(string q, int limit = 10);
    }
}
