namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    public class FactoryTestsCases
    {
        #region Private Fields

        private IOrganizationServiceFactory[] factories = new IOrganizationServiceFactory[]
        {
            CreateBareInputFactory(),
            CreateNoInputFactory(),
            CreateSerializedInputFactory(),
            CreateTransparentInputFactory(),
            CreateWrappedInputFactory()
        };

        private object[][] factoryTypes = new object[][]
        {
            new object[] { CreateBareInputFactory(), InstanceType.BareInput },
            new object[] { CreateNoInputFactory(), InstanceType.NoInput },
            new object[] { CreateSerializedInputFactory(), InstanceType.SerializedInput },
            new object[] { CreateTransparentInputFactory(), InstanceType.TransparentInput },
            new object[] { CreateWrappedInputFactory(), InstanceType.WrappedInput }
        };

        #endregion Private Fields

        #region Public Methods

        [TestCaseSource("factoryTypes"), Category("Factory")]
        public void Check_Service_Type(IOrganizationServiceFactory factory, InstanceType expected)
        {
            // Assert
            ((CuteFactory)factory).Provider.Type.Should().Be(expected);
        }

        [TestCaseSource("factories"), Category("Factory")]
        public virtual void Get_OrganizationService(IOrganizationServiceFactory factory)
        {
            // Act
            var userId = Guid.NewGuid();
            var service = factory.CreateOrganizationService(userId);

            // Assert
            service.Should().NotBeNull();

            if (((CuteService)service).Provider.Type == InstanceType.NoInput ||
                ((CuteService)service).Provider.Type == InstanceType.SerializedInput)
            {
                ((CuteService)service).Original.Should().BeNull();
            }
            else
            {
                ((CuteService)service).Original.Should().NotBeNull();
            }

            ((CuteService)service).UserId.Should().Be(userId);

            service.GetType().Should().BeAssignableTo<CuteService>();
            service.GetType().Should().BeAssignableTo<IOrganizationService>();
        }

        #endregion Public Methods

        #region Private Methods

        private static IOrganizationServiceFactory CreateBareInputFactory()
        {
            // Arrange
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            var provider = new CuteProvider(originalProvider);
            return (IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory));
        }

        private static IOrganizationServiceFactory CreateNoInputFactory()
        {
            return (IOrganizationServiceFactory)new CuteProvider().GetService(typeof(IOrganizationServiceFactory));
        }

        private static IOrganizationServiceFactory CreateSerializedInputFactory()
        {
            return (IOrganizationServiceFactory)new CuteProvider(new CuteProvider().ToBase64String()).GetService(typeof(IOrganizationServiceFactory));
        }

        private static IOrganizationServiceFactory CreateStandaloneInputFactory()
        {
            throw new NotImplementedException();
        }

        private static IOrganizationServiceFactory CreateTransparentInputFactory()
        {
            return (IOrganizationServiceFactory)new CuteProvider(Substitute.For<IOrganizationService>()).GetService(typeof(IOrganizationServiceFactory));
        }

        private static IOrganizationServiceFactory CreateWrappedInputFactory()
        {
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            var provider = new CuteProvider(originalProvider);
            return (IOrganizationServiceFactory)new CuteProvider(provider).GetService(typeof(IOrganizationServiceFactory));
        }

        #endregion Private Methods
    }
}