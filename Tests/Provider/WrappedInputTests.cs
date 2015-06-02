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
            // Arrange Act
            var originalProvider = Substitute.For<IServiceProvider>();
            var wrappedProvider = new WrappedProvider(originalProvider);
            var provider = new WrappedProvider(wrappedProvider);

            // Assert
            Assert.Equal(false, provider.Original is WrappedProvider);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_TracingService()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());

            var wrappedProvider = new WrappedProvider(originalProvider);
            var provider = new WrappedProvider(wrappedProvider);

            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            Assert.Equal(true, service is ITracingService);
        }

        [Fact(DisplayName = "Get WrappedContext")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_WrappedContext()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var wrappedProvider = new WrappedProvider(originalProvider);
            var provider = new WrappedProvider(wrappedProvider);

            // Act
            var context = provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.Equal(true, context is IPluginExecutionContext);
            Assert.Equal(true, context is CuteContext);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Wrapped Input")]
        public void Get_WrappedFactory()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var wrappedProvider = new WrappedProvider(originalProvider);
            var provider = new WrappedProvider(wrappedProvider);

            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.Equal(true, factory is IOrganizationServiceFactory);
            Assert.Equal(true, factory is CuteFactory);
        }

        #endregion Public Methods
    }
}