namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class WrappedInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public WrappedInputTests()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            var provider = new CuteProvider(originalProvider);
            
            provider = new CuteProvider(provider);
            this.Factory = (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Factory")]
        [Category("Wrapped Input")]
        public override void Get_OrganizationService()
        {
            base.Get_OrganizationService();
        }

        #endregion Public Methods
    }
}