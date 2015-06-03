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

    public class BareInputTests : ServiceTests
    {
        #region Public Methods

        [Fact(DisplayName = "Invoke Associate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Fact(DisplayName = "Invoke Create & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Delete")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Fact(DisplayName = "Invoke Disassociate")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Fact(DisplayName = "Invoke Execute & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Retrieve & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Fact(DisplayName = "Invoke RetrieveMultiple & Check Cache")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Fact(DisplayName = "Invoke Update")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public new void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}