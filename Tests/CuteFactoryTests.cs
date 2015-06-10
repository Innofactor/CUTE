namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class CuteFactoryTests
    {
        #region Public Methods

        [Test]
        [Category("Factory")]
        [Category("Bare Input")]
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
            Assert.IsInstanceOf<IOrganizationService>(service);
            Assert.IsInstanceOf<CuteService>(service);
            Assert.AreEqual(userId, ((CuteService)service).UserId);
        }

        #endregion Public Methods
    }
}