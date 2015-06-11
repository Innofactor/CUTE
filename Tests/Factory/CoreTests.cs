namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;
    using FluentAssertions;

    public class CoreTests : ICoreTests
    {
        #region Public Constructors

        public CoreTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            var provider = new CuteProvider(originalProvider);
            this.Factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
        }

        #endregion Public Constructors

        #region Public Properties

        public IOrganizationServiceFactory Factory
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Get_OrganizationService()
        {
            // Act
            var userId = Guid.NewGuid();
            var service = this.Factory.CreateOrganizationService(userId);

            // Assert
            service.Should().NotBeNull();
            ((CuteService)service).Original.Should().NotBeNull();
            ((CuteService)service).UserId.Should().Be(userId);
            service.GetType().Should().BeAssignableTo<CuteService>();
            service.GetType().Should().BeAssignableTo<IOrganizationService>();

            //Assert.NotNull(service);
            //Assert.NotNull(((CuteService)service).Original);
            //Assert.IsInstanceOf<IOrganizationService>(service);
            //Assert.IsInstanceOf<CuteService>(service);
            //Assert.AreEqual(userId, ((CuteService)service).UserId);
        }

        #endregion Public Methods
    }
}