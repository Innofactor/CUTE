namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests
    {
        #region Public Methods

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Bare Input")]
        public void Get_Context()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());
            provider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());

            // Act
            var context = provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsType<IPluginExecutionContext>(context);
            Assert.IsNotType<CuteContext>(context);
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public void Get_OriginalProvider()
        {
            // Arrange Act
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Assert
            Assert.IsNotType<CuteProvider>(provider.Original);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Bare Input")]
        public void Get_TracingService()
        {
            // Arrange
            var input = Substitute.For<IServiceProvider>();
            input.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
            var provider = new CuteProvider(input);

            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsType<ITracingService>(service);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Bare Input")]
        public void Get_WrappedFactory()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsType<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}