namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using System.Linq;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using Xunit;

    public class CoreTests : ICoreTests
    {
        #region Protected Fields

        protected Guid expectedResultCreate;

        protected OrganizationResponse expectedResultExecute;

        protected Entity expectedResultRetrieve;

        protected EntityCollection expectedResultRetrieveMultiple;

        #endregion Protected Fields

        #region Public Constructors

        public CoreTests()
        {
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

            this.Provider = new CuteProvider(originalProvider);
            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        public void Setup()
        {
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

            this.Provider = new CuteProvider(originalProvider);
            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Properties

        public CuteProvider Provider
        {
            get;
            protected set;
        }

        public IOrganizationService Service
        {
            get;
            protected set;
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Invoke_Associate()
        {
            // Act
            this.Service.Associate(string.Empty, Guid.Empty, new Relationship(), new EntityReferenceCollection());
        }

        public virtual void Invoke_Create_Check_Cache()
        {
            // Act
            var result = Service.Create(new Entity());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
            Assert.NotEqual<Guid>(Guid.Empty, result);
            Assert.Equal<Guid>(this.expectedResultCreate, result);
            Assert.Equal(1, this.Provider.Calls.Where(x => x.Message == MessageName.Create).Count());
        }

        public virtual void Invoke_Delete()
        {
            // Act
            this.Service.Delete(string.Empty, Guid.Empty);
        }

        public virtual void Invoke_Disassociate()
        {
            // Act
            this.Service.Disassociate(string.Empty, Guid.Empty, new Relationship(), new EntityReferenceCollection());
        }

        public virtual void Invoke_Execute_Check_Cache()
        {
            // Act
            var result = this.Service.Execute(new OrganizationRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OrganizationResponse>(result);
            Assert.Equal(this.expectedResultExecute.ResponseName, result.ResponseName);
            Assert.Equal(1, this.Provider.Calls.Where(x => x.Message == MessageName.Execute).Count());
        }

        public virtual void Invoke_Retrieve_Check_Cache()
        {
            // Act
            var result = this.Service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Entity>(result);
            Assert.NotEqual<Guid>(Guid.Empty, result.Id);
            Assert.Equal<Guid>(this.expectedResultRetrieve.Id, result.Id);
            Assert.Equal(1, this.Provider.Calls.Where(x => x.Message == MessageName.Retrieve).Count());
        }

        public virtual void Invoke_RetrieveMultiple_Check_Cache()
        {
            // Act
            var result = this.Service.RetrieveMultiple(new QueryExpression());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<EntityCollection>(result);
            Assert.Equal(this.expectedResultRetrieveMultiple.Entities.Count, result.Entities.Count);
            Assert.Equal(1, this.Provider.Calls.Where(x => x.Message == MessageName.RetrieveMultiple).Count());
        }

        public virtual void Invoke_Update()
        {
            // Act
            this.Service.Update(new Entity());
        }

        #endregion Public Methods
    }
}