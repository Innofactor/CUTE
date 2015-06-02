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
        private Entity expectedResultRetrieve;
        private EntityCollection expectedResultRetrieveMultiple;
        private CuteProvider provider;
        private IOrganizationService service;
        private OrganizationResponse expectedResultExecute;

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

            this.expectedResultRetrieveMultiple = new EntityCollection();
            this.expectedResultRetrieveMultiple.Entities.Add(new Entity());
            this.expectedResultRetrieveMultiple.Entities.Add(new Entity());
            this.expectedResultRetrieveMultiple.Entities.Add(new Entity());
            this.expectedResultRetrieveMultiple.Entities.Add(new Entity());
            this.expectedResultRetrieveMultiple.Entities.Add(new Entity());

            this.expectedResultExecute = new OrganizationResponse()
            {
                ResponseName = "Test"
            };

            originalService.Create(Arg.Any<Entity>()).Returns(this.expectedResultCreate);
            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(this.expectedResultRetrieve);
            originalService.RetrieveMultiple(Arg.Any<QueryBase>()).Returns(this.expectedResultRetrieveMultiple);
            originalService.Execute(Arg.Any<OrganizationRequest>()).Returns(this.expectedResultExecute);

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

        [Fact(DisplayName = "Invoke RetrieveMultiple & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_RetrieveMultiple_Check_Cache()
        {
            // Act
            var result = this.service.RetrieveMultiple(new QueryExpression());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityCollection>(result);
            Assert.Equal(this.expectedResultRetrieveMultiple.Entities.Count, result.Entities.Count);
            Assert.Equal(1, this.provider.Calls.Where(x => x.MessageName == MessageName.RetrieveMultiple).Count());
        }

        [Fact(DisplayName = "Invoke Execute & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Execute_Check_Cache()
        {
            // Act
            var result = this.service.Execute(new OrganizationRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrganizationResponse>(result);
            Assert.Equal(this.expectedResultExecute.ResponseName, result.ResponseName);
            Assert.Equal(1, this.provider.Calls.Where(x => x.MessageName == MessageName.Execute).Count());
        }

        #endregion Public Methods
    }
}