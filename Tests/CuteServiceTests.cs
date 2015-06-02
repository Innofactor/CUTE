namespace Cinteros.Unit.Test.Extensions.Tests
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

    public class CuteServiceTests
    {
        [Fact(DisplayName = "Get UserId")]
        [Trait("Module", "Service")]
        public void Get_UserId()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());
            var factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));

            // Act
            var userId = new Guid();
            var service = factory.CreateOrganizationService(userId);
            // Act
            

            // Assert


        }
    }
}
