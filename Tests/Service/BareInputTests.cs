namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests
    {
        #region Private Fields

        private Guid expectedResultCreate;
        private CuteProvider provider;
        private IOrganizationService service;

        #endregion Private Fields

        #region Public Constructors

        public BareInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            this.expectedResultCreate = Guid.NewGuid();

            originalService.Create(Arg.Any<Entity>()).Returns(this.expectedResultCreate);

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            this.provider = new CuteProvider(originalProvider);
            this.service = ((IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Invoke Create")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Create()
        {
            // Act
            var result = this.service.Create(new Entity());

            // Assert
            Assert.NotEqual<Guid>(Guid.Empty, result);
            Assert.Equal<Guid>(this.expectedResultCreate, result);
        }

        [Fact(DisplayName = "Invoke Create & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Create_Check_Cache()
        {
            // Act
            var result = service.Create(new Entity());

            // Assert
            Assert.NotEqual<Guid>(Guid.Empty, result);
            Assert.Equal(2, this.provider.Calls.Count);
        }

        #endregion Public Methods
    }
}