namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;

    internal class NoInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public NoInputTests()
        {
            // Arrange
            var provider = new CuteProvider();
            this.Factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Factory")]
        [Category("No Input")]
        public override void Get_OrganizationService()
        {
            // Act
            var userId = Guid.NewGuid();
            var service = this.Factory.CreateOrganizationService(userId);

            // Assert
            service.Should().NotBeNull();
            ((CuteService)service).Original.Should().BeNull();
            ((CuteService)service).UserId.Should().Be(userId);
            service.GetType().Should().BeAssignableTo<CuteService>();
            service.GetType().Should().BeAssignableTo<IOrganizationService>();
        }

        #endregion Public Methods
    }
}