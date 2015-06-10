namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using NUnit.Framework;

    public class SerializedInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public SerializedInputTests()
            : base ()
        {
            // Calling methods to fill calls stack
            this.Service.Create(new Entity());
            this.Service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());
            this.Service.RetrieveMultiple(new QueryExpression());
            this.Service.Execute(new OrganizationRequest());

            // Recreating provider from serialized one
            this.Provider = new CuteProvider(this.Provider.ToString());
            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Serialized Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}