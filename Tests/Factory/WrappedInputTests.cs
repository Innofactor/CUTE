namespace Cinteros.Unit.Test.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class WrappedInputTests
    {
        #region Public Methods

        [Fact(DisplayName = "Get OrganizationService")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_OrganizationService()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());

            var wrappedProvider = new CuteProvider(originalProvider);
            var provider = new CuteProvider(wrappedProvider);

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