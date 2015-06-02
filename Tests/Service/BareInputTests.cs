namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class BareInputTests
    {
        [Fact(DisplayName = "Invoke Create")]
        [Trait("Module", "Service")]
        [Trait("Provider", "Bare Input")]
        public void Invoke_Create()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());
            var factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(Guid.Empty);

            // Act
            var result = service.Create(new Entity());

            // Assert
            Assert.NotEqual<Guid>(Guid.Empty, result);
        }
    }
}
