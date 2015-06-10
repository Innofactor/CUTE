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

    public class ProxyInputTests
    {
        [Test]
        [Category("Provider")]
        [Category("Proxy Input")]
        public void Check_Uri_Format([Values("", "http://", "|")] string url)
        {
            // Arrange
            // Act
            var test = new TestDelegate(() =>
                {
                    var provider = new CuteProvider(new ClientCredentials(), new Uri(url));
                });
            
            // Assert
            Assert.Throws<UriFormatException>(test);
        }

        [Test]
        [Category("Provider")]
        [Category("Proxy Input")]
        public void Check_Uri_Values(
            [Values(
                "https://server/organization", 
                "https://server/organization/", 
                "https://server/organization/XRMServices",
                "https://server/organization/XRMServices/",
                "https://server/organization/XRMServices/2011",
                "https://server/organization/XRMServices/2011/",
                "https://server/organization/XRMServices/2011/Organization.svc")] string url)
        {
            // Arrange
            // Act
            var provider = new CuteProvider(new ClientCredentials(), new Uri(url));

            // Assert
            Assert.AreEqual("https://server/organization/XRMServices/2011/Organization.svc", provider.Endpoint);
        }
    }
}
