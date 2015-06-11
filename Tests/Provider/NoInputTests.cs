namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;
    using FluentAssertions;

    public class NoInputTests : CoreTests, ICoreTests
    {
        [SetUp]
        public void Setup()
        {
            base.Setup();
            this.Provider = new CuteProvider();
        }

        [Test]
        [Category("Provider"), Category("No Input")]
        public new void Check_Online_Status()
        {
            // Assert
            this.Provider.IsOnline.Should().BeFalse();
            // Assert.False(this.Provider.IsOnline);
        }

        [Test]
        [Category("Provider"), Category("No Input"), Category("Context")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            context.GetType().Should().BeAssignableTo<IPluginExecutionContext>();
            context.GetType().Should().BeAssignableTo<CuteContext>();
        }

        [Test]
        [Category("Provider"), Category("No Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            this.Provider.Original.Should().BeNull();
        }

        [Test]
        [Category("Provider"), Category("No Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            service.Should().NotBeNull();
            service.Should().BeAssignableTo<ITracingService>();
        }

        [Test]
        [Category("Provider"), Category("No Input"), Category("Factory")]
        public override void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

    }
}
