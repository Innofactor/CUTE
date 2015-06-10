﻿namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using System.Linq;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using FluentAssertions;
    using FluentAssertions.Equivalency;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using MatchPath = FluentAssertions.Equivalency.EquivalencyAssertionOptionsBase<FluentAssertions.Equivalency.EquivalencyAssertionOptions<Microsoft.Xrm.Sdk.EntityCollection>>;

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
            var result = this.Service.Create(new Entity());

            // Assert
            result.GetType().Should().Be<Guid>(because: "Create should return id of record created.");
            result.Should().NotBe(Guid.Empty, because: "Record Id cannot be empty Guid.");
            result.Should().Be(this.expectedResultCreate);

            this.Provider.Calls.Where(x => x.Message == MessageName.Create).Count().Should().Be(1, because: "Should be only one cached Create call");
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
            result.Should().NotBe(null);
            result.GetType().Should().Be<OrganizationResponse>();
            result.ShouldBeEquivalentTo(this.expectedResultExecute, options => options.Excluding(x => x.ExtensionData));

            this.Provider.Calls.Where(x => x.Message == MessageName.Execute).Count().Should().Be(1, because: "Should be only one cached Execute call");
        }

        public virtual void Invoke_Retrieve_Check_Cache()
        {
            // Act
            var result = this.Service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());

            // Assert
            result.Should().NotBe(null);
            result.ShouldBeEquivalentTo(this.expectedResultRetrieve, options => options.Excluding(x => x.ExtensionData));

            this.Provider.Calls.Where(x => x.Message == MessageName.Retrieve).Count().Should().Be(1);
        }

        public virtual void Invoke_RetrieveMultiple_Check_Cache()
        {
            // Act
            var result = this.Service.RetrieveMultiple(new QueryExpression());

            // Assert
            result.Should().NotBe(null);
            result.GetType().Should().Be<EntityCollection>();
            result.ShouldBeEquivalentTo<EntityCollection>(this.expectedResultRetrieveMultiple, options => ((MatchPath)options).Excluding(x => x.SelectedMemberPath.EndsWith("ExtensionData")));

            this.Provider.Calls.Where(x => x.Message == MessageName.RetrieveMultiple).Count().Should().Be(1);
        }

        public virtual void Invoke_Update()
        {
            // Act
            this.Service.Update(new Entity());
        }

        #endregion Public Methods
    }
}