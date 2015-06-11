namespace Cinteros.Unit.Testing.Extensions.Core
{
    using Microsoft.Xrm.Sdk;

    public class CuteTracing : ITracingService
    {
        #region Public Methods

        void ITracingService.Trace(string format, params object[] args)
        {
            // Do nothing
        }

        #endregion Public Methods
    }
}