﻿namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using Xunit;

    public class SerializedInputTests : IServiceTests
    {
        #region Private Fields

        private Guid expectedResultCreate;

        private OrganizationResponse expectedResultExecute;

        private Entity expectedResultRetrieve;

        private EntityCollection expectedResultRetrieveMultiple;

        #endregion Private Fields

        #region Public Constructors

        public SerializedInputTests()
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

            this.Service.Create(new Entity());
            this.Service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());
            this.Service.RetrieveMultiple(new QueryExpression());
            this.Service.Execute(new OrganizationRequest());

            this.Provider = new CuteProvider(this.Provider.ToString());
            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Properties

        public CuteProvider Provider
        {
            get;
            private set;
        }

        public IOrganizationService Service
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        [Fact(DisplayName = "Invoke Associate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Associate()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Create & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Create_Check_Cache()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Delete")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Delete()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Disassociate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Disassociate()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Execute & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Execute_Check_Cache()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Retrieve & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Retrieve_Check_Cache()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke RetrieveMultiple & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_RetrieveMultiple_Check_Cache()
        {
            throw new NotImplementedException();
        }

        [Fact(DisplayName = "Invoke Update")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Serialized Input")]
        public void Invoke_Update()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}