namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class ProviderTestCases
    {
        #region Private Fields

        private IServiceProvider[] providers = new IServiceProvider[]
        {
            ProviderTestCases.CreateBareInputProvider(),
            ProviderTestCases.CreateNoInputProvider(),
            ProviderTestCases.CreateSerializedInputProvider(),
            ProviderTestCases.CreateTransparentInputProvider(),
            ProviderTestCases.CreateWrappedInputProvider()
        };

        private object[][] providerTypes = new object[][]
        {
            new object[] { ProviderTestCases.CreateBareInputProvider(), InstanceType.BareInput },
            new object[] { ProviderTestCases.CreateNoInputProvider(), InstanceType.NoInput },
            new object[] { ProviderTestCases.CreateSerializedInputProvider(), InstanceType.SerializedInput },
            new object[] { ProviderTestCases.CreateTransparentInputProvider(), InstanceType.TransparentInput },
            new object[] { ProviderTestCases.CreateWrappedInputProvider(), InstanceType.WrappedInput }
        };

        #endregion Private Fields

        #region Public Properties

        public CuteProvider Provider
        {
            get;
            protected set;
        }

        #endregion Public Properties

        #region Public Methods

        [TestCaseSource("providerTypes"), Category("Provider")]
        public void Check_Service_Type(IServiceProvider provider, InstanceType expected)
        {
            // Assert
            ((CuteProvider)provider).Type.Should().Be(expected);
        }

        [TestCaseSource("providers"), Category("Provider")]
        public virtual void Get_Context(IServiceProvider provider)
        {
            // Act
            var context = provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            context.Should().BeAssignableTo<IPluginExecutionContext>();

            if (((CuteProvider)provider).Type == InstanceType.NoInput ||
                ((CuteProvider)provider).Type == InstanceType.SerializedInput ||
                ((CuteProvider)provider).Type == InstanceType.TransparentInput)
            {
                context.GetType().Should().Be<CuteContext>();
            }
            else
            {
                context.GetType().Should().NotBe<CuteContext>();
            }
        }

        [TestCaseSource("providers"), Category("Provider")]
        public virtual void Get_OriginalProvider(IServiceProvider provider)
        {
            // Assert
            if (((CuteProvider)provider).Type == InstanceType.NoInput ||
                ((CuteProvider)provider).Type == InstanceType.SerializedInput ||
                ((CuteProvider)provider).Type == InstanceType.TransparentInput)
            {
                ((CuteProvider)provider).Original.Should().BeNull();
            }
            else
            {
                ((CuteProvider)provider).Original.Should().NotBeNull();
                ((CuteProvider)provider).Original.GetType().Should().NotBe<CuteProvider>();
            }
        }

        [TestCaseSource("providers"), Category("Provider")]
        public virtual void Get_TracingService(IServiceProvider provider)
        {
            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            service.GetType().Should().BeAssignableTo<ITracingService>();
        }

        [TestCaseSource("providers"), Category("Provider")]
        public virtual void Get_WrappedFactory(IServiceProvider provider)
        {
            // Act
            var factory = provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            factory.GetType().Should().BeAssignableTo<IOrganizationServiceFactory>();
            factory.GetType().Should().BeAssignableTo<CuteFactory>();
        }

        #endregion Public Methods

        #region Private Methods

        private static IServiceProvider CreateBareInputProvider()
        {
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());
            provider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());
            provider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());

            return provider;
        }

        private static IServiceProvider CreateNoInputProvider()
        {
            return new CuteProvider();
        }

        private static IServiceProvider CreateSerializedInputProvider()
        {
            var provider = ProviderTestCases.CreateBareInputProvider();
            return new CuteProvider(((CuteProvider)provider).ToBase64String());
        }

        private static IServiceProvider CreateTransparentInputProvider()
        {
            return new CuteProvider(Substitute.For<IOrganizationService>());
        }

        private static IServiceProvider CreateWrappedInputProvider()
        {
            var provider = ProviderTestCases.CreateBareInputProvider();
            return new CuteProvider(provider);
        }

        #endregion Private Methods
    }
}