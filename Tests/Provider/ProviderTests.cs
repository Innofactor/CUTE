namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    public interface IProviderTests
    {
        #region Public Methods

        void Check_Online_Status();

        void Get_Context();

        void Get_OriginalProvider();

        void Get_TracingService();

        void Get_WrappedFactory();

        #endregion Public Methods
    }
}