namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class CuteFactoryTests
    {
        #region Public Methods

        [Fact(DisplayName = "Get OrganizationService")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Bare Input")]
        public void Get_OrganizationService()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            var provider = new CuteProvider(originalProvider);
            var factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));

            // Act
            var userId = Guid.NewGuid();
            var service = factory.CreateOrganizationService(userId);

            // Assert
            Assert.NotNull(service);
            Assert.IsAssignableFrom<IOrganizationService>(service);
            Assert.IsType<CuteService>(service);
            Assert.Equal(userId, ((CuteService)service).UserId);
        }

        #endregion Public Methods
    }
}