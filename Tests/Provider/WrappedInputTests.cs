namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Xunit;

    public class WrappedInputTests : CoreTests
    {
        #region Public Constructors

        public WrappedInputTests()
            : base()
        {
            this.Provider = new CuteProvider(this.Provider);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public override void Check_Online_Status()
        {
            base.Check_Online_Status();
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Wrapped Input")]
        public override void Get_Context()
        {
            base.Get_Context();
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public override void Get_OriginalProvider()
        {
            base.Get_OriginalProvider();
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public override void Get_TracingService()
        {
            base.Get_TracingService();
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Wrapped Input")]
        public override void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        #endregion Public Methods
    }
}