namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class WrappedInputTests : ProviderTests
    {
        #region Public Constructors

        public WrappedInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
            originalProvider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());

            var wrappedProvider = new CuteProvider(originalProvider);
            this.Provider = new CuteProvider(wrappedProvider);
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public override void Check_Online_Status()
        {
            // Assert
            Assert.True(this.Provider.IsOnline);
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