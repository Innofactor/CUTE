namespace Cinteros.Unit.Test.Extensions.Core
{
    using System;
    using Microsoft.Xrm.Sdk;

    public class CuteFactory : IOrganizationServiceFactory
    {
        #region Private Fields

        private CuteProvider provider;

        #endregion Private Fields

        #region Public Constructors

        public CuteFactory(CuteProvider provider)
        {
            this.provider = provider;
        }

        #endregion Public Constructors

        #region Public Methods

        IOrganizationService IOrganizationServiceFactory.CreateOrganizationService(Guid? userId)
        {
            return new CuteService(this.provider, userId);
        }

        #endregion Public Methods
    }
}