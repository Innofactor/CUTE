namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests : ProviderTests
    {
        #region Public Constructors

        public BareInputTests()
            : base()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public new void Check_Online_Status()
        {
            base.Check_Online_Status();
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Bare Input")]
        public new void Get_Context()
        {
            base.Get_Context();
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public new void Get_OriginalProvider()
        {
            base.Get_OriginalProvider();
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public new void Get_TracingService()
        {
            base.Get_TracingService();
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Bare Input")]
        public new void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        #endregion Public Methods
    }
}