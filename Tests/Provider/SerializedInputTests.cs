namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class SerializedInputTests : IProviderTests
    {
        #region Private Fields

        private CuteProvider provider;

        #endregion Private Fields

        #region Public Constructors

        public SerializedInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
            originalProvider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());

            var wrappedProvider = new CuteProvider(originalProvider);
            this.provider = new CuteProvider(wrappedProvider.ToString());
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Serialized Input")]
        public void Check_Online_Status()
        {
            // Assert
            Assert.False(this.provider.IsOnline);
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "Serialized Input")]
        public void Get_Context()
        {
            // Act
            var context = this.provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<IPluginExecutionContext>(context);
            Assert.IsType<CuteContext>(context);
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Serialized Input")]
        public void Get_OriginalProvider()
        {
            // Assert
            Assert.IsNotType<CuteProvider>(this.provider.Original);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "Serialized Input")]
        public void Get_TracingService()
        {
            // Act
            var service = this.provider.GetService(typeof(ITracingService));

            // Assert
            Assert.NotNull(service);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "Serialized Input")]
        public void Get_WrappedFactory()
        {
            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsAssignableFrom<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}