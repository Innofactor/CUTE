namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using Microsoft.Xrm.Sdk;

    internal interface ICoreTests
    {
        #region Public Properties

        IOrganizationServiceFactory Factory { get; set; }

        #endregion Public Properties

        #region Public Methods

        void Get_OrganizationService();

        void Setup();

        #endregion Public Methods
    }
}