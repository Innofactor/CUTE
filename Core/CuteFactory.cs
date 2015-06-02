namespace Cinteros.Unit.Test.Extensions.Core
{
    using System;
    using Microsoft.Xrm.Sdk;

    public class CuteFactory : IOrganizationServiceFactory
    {
        #region Public Methods

        IOrganizationService IOrganizationServiceFactory.CreateOrganizationService(Guid? userId)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}