namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class WrappedInputTests
    {
        #region Public Methods

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_OriginalProvider()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var wrappedProvider = new CuteProvider(originalProvider);

            // Act
            var provider = new CuteProvider(wrappedProvider);

            // Assert
            Assert.False(provider.Original is CuteProvider);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_TracingService()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());

            var wrappedProvider = new CuteProvider(originalProvider);
            var provider = new CuteProvider(wrappedProvider);

            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsType<ITracingService>(service);
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_Context()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());
            
            var wrappedProvider = new CuteProvider(originalProvider);
            var provider = new CuteProvider(wrappedProvider);

            // Act
            var context = provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsType<IPluginExecutionContext>(context);
            Assert.False(context is CuteContext);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_WrappedFactory()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var wrappedProvider = new CuteProvider(originalProvider);
            var provider = new CuteProvider(wrappedProvider);

            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsType<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}