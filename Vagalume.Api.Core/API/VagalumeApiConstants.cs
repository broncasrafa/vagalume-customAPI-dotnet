namespace Vagalume.Api.Core.API
{
    public class VagalumeApiConstants
    {
        

        public const string VAGALUME_URL = "https://www.vagalume.com.br";

        public const string TOP100_MUSICAS_URL = VAGALUME_URL + "/top100/musicas";
        public const string TOP100_ARTISTAS_URL = VAGALUME_URL + "/top100/artistas";
        public const string TOP100_ALBUMS_URL = VAGALUME_URL + "/top100/albuns";

        public const string ARTISTA_URL = VAGALUME_URL + "/{0}";
        public const string ARTISTA_LETRAS_URL = ARTISTA_URL;
        public const string ARTISTA_ALBUMS_URL = ARTISTA_URL + "/discografia/";
        public const string ARTISTA_FOTOS_URL = ARTISTA_URL + "/fotos/";
        public const string ARTISTA_RELACIONADOS_URL = ARTISTA_URL + "/relacionados/";
        public const string ARTISTA_BIOGRAFIA_URL = ARTISTA_URL + "/biografia/";

        public const string ALBUM_URL = VAGALUME_URL + "/{0}/discografia/{1}.html";

        public const string MUSICA_URL = VAGALUME_URL + "/{0}/{1}.html";

        // API
        private const string API_KEY = "7d928a5211f7f87ddcc09ff234596701";

        public const string API_BASE_URL = "https://api.vagalume.com.br";
        public const string API_ARTISTA_URL = VAGALUME_URL + "/{0}/index.js";

        public const string API_FOTOS_ARTISTA_URL = "https://api.vagalume.com.br/image.php?bandID={0}&limit={1}&apikey=" + API_KEY;

        public const string API_BUSCAR_ARTISTA_URL = API_BASE_URL + "/search.art?q={0}&limit={1}&apikey=" + API_KEY;
        public const string API_BUSCAR_TRECHO_MUSICA_URL = API_BASE_URL + "/search.excerpt?q={0}&limit={1}&apikey=" + API_KEY;
        public const string API_BUSCAR_MUSICAS_ARTISTA_URL = API_BASE_URL + "/search.artmus?q={0}&limit={1}&apikey=" + API_KEY;
    }
}


// https://www.vagalume.com.br/racionais-mcs/discografia/nada-como-um-dia-apos-o-outro-dia.html

// https://www.vagalume.com.br/dua-lipa/dont-start-now-traducao.html

// https://www.vagalume.com.br/u2/index.js

// https://api.vagalume.com.br/image.php?bandID=3ade68b7g2d6e1ea3&limit=10&apikey=7d928a5211f7f87ddcc09ff234596701

// https://api.vagalume.com.br/search.php?art=lana-del-rey&mus=Cherry&apikey=7d928a5211f7f87ddcc09ff234596701