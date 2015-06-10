namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    internal class TransparentInputTests : CoreTests, ICoreTests
    {
        #region Public Methods

        [Test]
        [Category("Factory")]
        [Category("Transparent Input")]
        public override void Get_OrganizationService()
        {
            // Act
            var userId = Guid.NewGuid();
            var service = this.Factory.CreateOrganizationService(userId);

            // Assert
            Assert.NotNull(service);
            Assert.NotNull(((CuteService)service).Original);
            Assert.IsInstanceOf<IOrganizationService>(service);
            Assert.IsInstanceOf<CuteService>(service);
            Assert.AreEqual(userId, ((CuteService)service).UserId);
        }

        [SetUp]
        public override void Setup()
        {
            // Arrange



            var provider = new CuteProvider(Substitute.For<IOrganizationService>());

            this.Factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
        }

        #endregion Public Methods
    }
}