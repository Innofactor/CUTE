namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Test.Extensions.Core;
    using Cinteros.Unit.Test.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using Xunit;

    public class NoInputTests : CoreTests
    {
        public NoInputTests()
        {
            // Creating provider from scratch
            this.Provider = new CuteProvider();

            this.Provider.Calls.Add(new CuteCall(MessageName.Create, new[] { new Entity() }, this.expectedResultCreate));
            this.Provider.Calls.Add(new CuteCall(MessageName.Retrieve, new object[] { string.Empty, Guid.Empty, new ColumnSet() }, this.expectedResultRetrieve));
            this.Provider.Calls.Add(new CuteCall(MessageName.RetrieveMultiple, new object[] { new QueryExpression() }, this.expectedResultRetrieveMultiple));
            this.Provider.Calls.Add(new CuteCall(MessageName.Execute, new object[] { new OrganizationRequest() }, this.expectedResultExecute));

            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #region Public Methods

        [Fact(DisplayName = "Invoke Associate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Fact(DisplayName = "Invoke Create & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Delete")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Fact(DisplayName = "Invoke Disassociate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Fact(DisplayName = "Invoke Execute & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Retrieve & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Fact(DisplayName = "Invoke RetrieveMultiple & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Update")]
        [Trait("Module", "Service")]
        [Trait("Provider", "No Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}
