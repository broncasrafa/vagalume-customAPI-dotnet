using Vagalume.Api.Core.API;
using Vagalume.Api.Core.API.Builder;

namespace Vagalume.Api.Service
{
    public class VagalumeService
    {
        private readonly IVagalumeApi _vagalumeApi;

        public VagalumeService()
        {
            _vagalumeApi = VagalumeApiBuilder.CreateBuilder().Build();
        }

        public IVagalumeApi InitializeApiInstance()
        {
            return _vagalumeApi;
        }


        #region [ Album ]
        #endregion

        #region [ Artist ]
        #endregion

        #region [ Music ]
        #endregion

        #region [ Search ]
        #endregion
    }
}