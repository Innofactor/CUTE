namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Xunit;

    public class NoInputTests : CoreTests
    {
        public NoInputTests()
        {
            this.Provider = new CuteProvider();
        }

        [Fact(DisplayName = "Check Online Status")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "No Input")]
        public new void Check_Online_Status()
        {
            // Assert
            Assert.False(this.Provider.IsOnline);
        }

        [Fact(DisplayName = "Get Context")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        [Trait("Provider", "No Input")]
        public new void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<IPluginExecutionContext>(context);
            Assert.IsType<CuteContext>(context);
        }

        [Fact(DisplayName = "Get OriginalProvider")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "No Input")]
        public new void Get_OriginalProvider()
        {
            // Assert
            Assert.Null(this.Provider.Original);
        }

        [Fact(DisplayName = "Get TracingService")]
        [Trait("Module", "Provider")]
        [Trait("Provider", "No Input")]
        public new void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.NotNull(service);
        }

        [Fact(DisplayName = "Get WrappedFactory")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Factory")]
        [Trait("Provider", "No Input")]
        public override void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

    }
}
