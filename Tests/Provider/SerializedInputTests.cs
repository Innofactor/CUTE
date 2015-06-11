namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;

    public class SerializedInputTests : CoreTests, ICoreTests
    {
        #region Public Methods

        [Test]
        [Category("Provider"), Category("Serialized Input")]
        public new void Check_Online_Status()
        {
            // Assert
            this.Provider.IsOnline.Should().BeFalse();
        }

        [Test]
        [Category("Provider"), Category("Serialized Input"), Category("Context")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            context.Should().NotBeNull();
            context.Should().BeAssignableTo<IPluginExecutionContext>();
            context.Should().BeAssignableTo<CuteContext>();
        }

        [Test]
        [Category("Provider"), Category("Serialized Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            this.Provider.Original.Should().BeNull();
        }

        [Test]
        [Category("Provider"), Category("Serialized Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            service.Should().NotBeNull();
            service.Should().BeAssignableTo<ITracingService>();
            service.Should().BeAssignableTo<CuteTracing>();
        }

        [Test]
        [Category("Provider"), Category("Serialized Input"), Category("Factory")]
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