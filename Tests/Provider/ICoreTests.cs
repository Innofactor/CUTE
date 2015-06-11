namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;

    internal interface ICoreTests
    {
        #region Public Properties

        CuteProvider Provider { get; }

        #endregion Public Properties

        #region Public Methods

        void Get_Context();

        void Get_OriginalProvider();

        void Get_TracingService();

        void Get_WrappedFactory();

        #endregion Public Methods
    }
}