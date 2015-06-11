namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class TransparentInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public TransparentInputTests()
            : base()
        {
            this.Provider = new CuteProvider(Substitute.For<IOrganizationService>());
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            context.GetType().Should().BeAssignableTo<CuteContext>();
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            this.Provider.Original.Should().BeNull();
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            service.Should().NotBeNull();
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        #endregion Public Methods
    }
}