using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vagalume.Api.Core.API.Classes;
using Vagalume.Api.Core.API.Providers;
using Vagalume.Api.Domain;

namespace Vagalume.Api.Core.API
{
    internal class VagalumeApi : IVagalumeApi
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;
        private IMusicProvider _musicProvider;
        private IArtistProvider _artistProvider;
        private IAlbumProvider _albumProvider;
        private ISearchProvider _searchProvider;

        public VagalumeApi(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
            InitializeProviders();
        }

        private void InitializeProviders()
        {
            _musicProvider = new MusicProvider(_httpRequestProcessor);
            _artistProvider = new ArtistProvider(_httpRequestProcessor);
            _albumProvider = new AlbumProvider(_httpRequestProcessor);
            _searchProvider = new SearchProvider(_httpRequestProcessor);
        }

        public async Task<IResult<Album>> GetAlbum(string artId, string albId)
        {
            if (string.IsNullOrEmpty(artId) || string.IsNullOrEmpty(albId))
                throw new ArgumentException("ArtId and albId must be specified");

            return await _albumProvider.GetAlbum(artId, albId);
        }
               
        public async Task<IResult<Artista>> GetArtista(string art)
        {
            if (string.IsNullOrEmpty(art))
                throw new ArgumentException("Art (artistId) must be specified");

            return await _artistProvider.GetArtista(art);
        }
               
        public async Task<IResult<Biografia>> GetBiografiaArtista(string artId)
        {
            if (string.IsNullOrEmpty(artId))
                throw new ArgumentException("ArtId (artistId) must be specified");

            return await _artistProvider.GetBiografiaArtista(artId);
        }
               
        public async Task<IResult<Discografia>> GetDiscografiaArtista(string artId)
        {
            if (string.IsNullOrEmpty(artId))
                throw new ArgumentException("ArtId (artistId) must be specified");

            return await _artistProvider.GetDiscografiaArtista(artId);
        }
               
        public async Task<IResult<List<FotoArtista>>> GetFotosArtista(string artId)
        {
            if (string.IsNullOrEmpty(artId))
                throw new ArgumentException("ArtId (artistId) must be specified");

            return await _artistProvider.GetFotosArtista(artId);
        }

        public async Task<IResult<List<FotoArtista>>> GetFotosArtistaApi(string bandId, int limit = 10)
        {
            if (string.IsNullOrEmpty(bandId))
                throw new ArgumentException("BandId (artistId) must be specified");

            return await _artistProvider.GetFotosArtistaApi(bandId, limit);
        }

        public async Task<IResult<LetraMusica>> GetLetraMusica(string artista, string musica)
        {
            if (string.IsNullOrEmpty(artista) || string.IsNullOrEmpty(musica))
                throw new ArgumentException("Artist name and music name must be specified");

            return await _musicProvider.GetLetraMusica(artista, musica);
        }

        public async Task<IResult<List<TopAlbums>>> GetTop100Albums(string tipo, int mes, int ano)
        {
            if (string.IsNullOrEmpty(tipo))
                throw new ArgumentException("Tipo must be specified");
            if (!IsMesValido(mes))
                throw new ArgumentException("Invalid month");
            if (!IsAnoValido(ano))
                throw new ArgumentException("Invalid year");

            return await _albumProvider.GetTop100Albums(tipo, mes, ano);
        }

        public async Task<IResult<List<TopArtists>>> GetTop100Artistas(string tipo, int mes, int ano)
        {
            if (string.IsNullOrEmpty(tipo))
                throw new ArgumentException("Tipo must be specified");
            if (!IsMesValido(mes))
                throw new ArgumentException("Invalid month");
            if (!IsAnoValido(ano))
                throw new ArgumentException("Invalid year");

            return await _artistProvider.GetTop100Artistas(tipo, mes, ano);
        }

        public async Task<IResult<List<TopMusics>>> GetTop100Musicas(string tipo, int mes, int ano)
        {
            if (string.IsNullOrEmpty(tipo))
                throw new ArgumentException("Tipo must be specified");
            if (!IsMesValido(mes))
                throw new ArgumentException("Invalid month");
            if (!IsAnoValido(ano))
                throw new ArgumentException("Invalid year");

            return await _musicProvider.GetTop100Musicas(tipo, mes, ano);
        }

        public async Task<IResult<List<DocBusca>>> PesquisarArtistas(string q, int limit = 10)
        {
            return await _searchProvider.PesquisarArtistasAsync(q, limit);
        }

        public async Task<IResult<List<DocBusca>>> PesquisarTrechoMusica(string q, int limit = 10)
        {
            return await _searchProvider.PesquisarTrechoMusica(q, limit);
        }

        public async Task<IResult<List<DocBusca>>> PesquisarMusicasArtistas(string q, int limit = 10)
        {
            return await _searchProvider.PesquisarMusicasArtistas(q, limit);
        }


        private bool IsMesValido(int mes)
        {
            return mes > 0 && mes < 13;
        }
        private bool IsAnoValido(int ano)
        {
            return ano > 2003;
        }

        
    }
}
