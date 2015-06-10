namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    internal interface ICoreTests
    {
        #region Public Properties

        Cinteros.Unit.Testing.Extensions.Core.CuteProvider Provider { get; }

        #endregion Public Properties

        #region Public Methods

        void Check_Online_Status();

        void Get_Context();

        void Get_OriginalProvider();

        void Get_TracingService();

        void Get_WrappedFactory();

        void Setup();

        #endregion Public Methods
    }
}