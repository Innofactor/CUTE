namespace Cinteros.Unit.Test.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests
    {
        #region Public Methods

        [Fact(DisplayName = "Get OrganizationService")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Bare Input")]
        public void Get_OrganizationService()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());
            var factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));

            // Act
            var service = factory.CreateOrganizationService(new Guid());

            // Assert
            Assert.NotNull(service);
            Assert.IsAssignableFrom<IOrganizationService>(service);
            Assert.IsType<CuteService>(service);
        }

        #endregion Public Methods
    }
}