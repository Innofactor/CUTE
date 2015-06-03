namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests : IProviderTests
    {
        #region Protected Fields

        public CuteProvider Provider
        {
            get;
            private set;
        }

        #endregion Protected Fields

        #region Public Constructors

        public BareInputTests()
        {
            // Arrange
            this.Provider = new CuteProvider(Substitute.For<IServiceProvider>());
            this.Provider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());
            this.Provider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public void Check_Online_Status()
        {
            // Assert
            Assert.True(this.Provider.IsOnline);
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Bare Input")]
        public void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<IPluginExecutionContext>(context);
            Assert.IsNotType<CuteContext>(context);
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public void Get_OriginalProvider()
        {
            // Assert
            Assert.IsNotType<CuteProvider>(this.Provider.Original);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsAssignableFrom<ITracingService>(service);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Bare Input")]
        public void Get_WrappedFactory()
        {
            // Act
            var factory = Provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsAssignableFrom<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}