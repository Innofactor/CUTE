namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;

    public class SerializedInputTests : CoreTests, ICoreTests
    {
        #region Public Methods

        [Test]
        [Category("Provider")]
        [Category("Serialized Input")]
        public new void Check_Online_Status()
        {
            // Assert
            Assert.False(this.Provider.IsOnline);
        }

        [Test]
        [Category("Provider")]
        [Category("Serialized Input")]
        [Category("Context")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsInstanceOf<IPluginExecutionContext>(context);
            Assert.IsAssignableFrom<CuteContext>(context);
        }

        [Test]
        [Category("Provider")]
        [Category("Serialized Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            Assert.Null(this.Provider.Original);
        }

        [Test]
        [Category("Provider")]
        [Category("Serialized Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.NotNull(service);
        }

        [Test]
        [Category("Provider")]
        [Category("Serialized Input")]
        [Category("Factory")]
        public override void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        [SetUp]
        public void Setup()
        {
            base.Setup();
            this.Provider = new CuteProvider(this.Provider.ToString());
        }

        #endregion Public Methods
    }
}