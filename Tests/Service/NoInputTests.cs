namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NUnit.Framework;

    public class NoInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public NoInputTests()
        {
            // Creating provider from scratch
            this.Provider = new CuteProvider();

            this.Provider.Calls.Add(new CuteCall(MessageName.Create, new[] { new Entity() }, this.expectedResultCreate));
            this.Provider.Calls.Add(new CuteCall(MessageName.Create, new[] { new Entity("fail") }, new InvalidPluginExecutionException()));

            this.Provider.Calls.Add(new CuteCall(MessageName.Retrieve, new object[] { string.Empty, Guid.Empty, new ColumnSet() }, this.expectedResultRetrieve));
            this.Provider.Calls.Add(new CuteCall(MessageName.RetrieveMultiple, new object[] { new QueryExpression() }, this.expectedResultRetrieveMultiple));
            this.Provider.Calls.Add(new CuteCall(MessageName.Execute, new object[] { new OrganizationRequest() }, this.expectedResultExecute));

            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("No Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}