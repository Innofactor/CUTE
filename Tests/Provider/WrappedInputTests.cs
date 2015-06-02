namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class WrappedInputTests
    {
        private CuteProvider wrappedProvider;

        public WrappedInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
            originalProvider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());

            this.wrappedProvider = new CuteProvider(originalProvider);
        }

        #region Public Methods

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_OriginalProvider()
        {
            // Act
            var provider = new CuteProvider(this.wrappedProvider);

            // Assert
            Assert.False(provider.Original is CuteProvider);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_TracingService()
        {
            // Arrange
            var provider = new CuteProvider(this.wrappedProvider);

            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsAssignableFrom<ITracingService>(service);
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_Context()
        {
            // Arrange
            var provider = new CuteProvider(this.wrappedProvider);

            // Act
            var context = provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<IPluginExecutionContext>(context);
            Assert.IsNotType<CuteContext>(context);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_WrappedFactory()
        {
            // Arrange
            var provider = new CuteProvider(this.wrappedProvider);

            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsAssignableFrom<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}