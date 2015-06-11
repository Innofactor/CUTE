namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class TransparentInputTests : CoreTests, ICoreTests
    {
        #region Public Methods

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Check_Online_Status()
        {
            // Assert
            Assert.True(this.Provider.IsOnline);
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<CuteContext>(context);
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            Assert.Null(this.Provider.Original);
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.NotNull(service);
        }

        [Test]
        [Category("Provider"), Category("Transparent Input")]
        public new void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        [SetUp]
        public new void Setup()
        {
            base.Setup();
            this.Provider = new CuteProvider(Substitute.For<IOrganizationService>());
        }

        #endregion Public Methods
    }
}