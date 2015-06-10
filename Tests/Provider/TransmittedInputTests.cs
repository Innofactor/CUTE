namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Description;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Testing.Extensions.Core;
    using NUnit.Framework;

    public class TransmittedInputTests
    {
        [Test]
        [Category("Provider")]
        [Category("Proxy Input")]
        public void Check_Endpoint(
            [Values(
                "https://server/organization", 
                "https://server/organization/",
                "https://server/organization/XRMServices/2011/Organization.svc")] string url)
        {
            // Arrange
            // Act
            var provider = new CuteProvider(new ClientCredentials(), new Uri(url));

            // Assert
            Assert.AreEqual("https://server/organization/XRMServices/2011/Organization.svc", provider.Endpoint);
        }

        [Test]
        [Category("Provider")]
        [Category("Proxy Input")]
        public void Check_User()
        {
            // Arrange
            var credentials = new ClientCredentials();
            credentials.UserName.UserName = "domain\\user";

            // Act
            var provider = new CuteProvider(credentials, new Uri("https://server/organization/XRMServices/2011/Organization.svc"));

            // Assert
            Assert.AreEqual("domain\\user", provider.User);
        }
    }
}
