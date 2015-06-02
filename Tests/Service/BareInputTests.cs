namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using System;
    using System.Linq;
    using Cinteros.Unit.Test.Extensions.Core;
    using Cinteros.Unit.Test.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using Xunit;

    public class BareInputTests
    {
        #region Private Fields

        private Guid expectedResultCreate;
        private CuteProvider provider;
        private IOrganizationService service;
        private Entity expectedResultRetrieve;

        #endregion Private Fields

        #region Public Constructors

        public BareInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            this.expectedResultCreate = Guid.NewGuid();
            this.expectedResultRetrieve = new Entity()
            {
                Id = Guid.NewGuid()
            };

            originalService.Create(Arg.Any<Entity>()).Returns(this.expectedResultCreate);
            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(this.expectedResultRetrieve);


            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            this.provider = new CuteProvider(originalProvider);
            this.service = ((IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Invoke Create & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Create_Check_Cache()
        {
            // Act
            var result = service.Create(new Entity());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
            Assert.NotEqual<Guid>(Guid.Empty, result);
            Assert.Equal<Guid>(this.expectedResultCreate, result);
            Assert.Equal(1, this.provider.Calls.Where(x => x.MessageName == MessageName.Create).Count());
        }

        [Fact(DisplayName = "Invoke Retrieve & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Retrieve_Check_Cache()
        {
            // Act
            var result = this.service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Entity>(result);
            Assert.NotEqual<Guid>(Guid.Empty, result.Id);
            Assert.Equal<Guid>(this.expectedResultRetrieve.Id, result.Id);
            Assert.Equal(1, this.provider.Calls.Where(x => x.MessageName == MessageName.Retrieve).Count());
        }

        #endregion Public Methods
    }
}