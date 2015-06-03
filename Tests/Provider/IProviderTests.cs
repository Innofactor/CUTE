namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using Cinteros.Unit.Test.Extensions.Core;

    public interface IProviderTests
    {
        #region Public Properties

        CuteProvider Provider
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods

        void Check_Online_Status();

        void Get_Context();

        void Get_OriginalProvider();

        void Get_TracingService();

        void Get_WrappedFactory();

        #endregion Public Methods
    }
}