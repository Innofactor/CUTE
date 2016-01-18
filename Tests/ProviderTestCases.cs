namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    public class ProviderTestCases
    {
        #region Private Fields

        private IServiceProvider[] providers = new IServiceProvider[]
        {
            CreateBareInputProvider(),
            CreateNoInputProvider(),
            CreateSerializedInputProvider(),
            CreateTransparentInputProvider(),
            CreateWrappedInputProvider()
        };

        private object[][] providerTypes = new object[][]
        {
            new object[] { CreateBareInputProvider(), InstanceType.BareInput },
            new object[] { CreateNoInputProvider(), InstanceType.NoInput },
            new object[] { CreateSerializedInputProvider(), InstanceType.SerializedInput },
            new object[] { CreateTransparentInputProvider(), InstanceType.TransparentInput },
            new object[] { CreateWrappedInputProvider(), InstanceType.WrappedInput }
        };

        #endregion Private Fields

        #region Public Methods

        [TestCaseSource("providerTypes"), Category("Provider")]
        public void Check_Service_Type(IServiceProvider provider, InstanceType expected)
        {
            // Assert
            ((CuteProvider)provider).Type.Should().Be(expected);
        }

        [TestCaseSource("providers"), Category("Provider"), Category("Context")]
        public virtual void Get_Context(IServiceProvider provider)
        {
            // Act
            var context = (IPluginExecutionContext)provider.GetService(typeof(IPluginExecutionContext));

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

            context.MessageName.Should().NotBeEmpty();
            context.PrimaryEntityName.Should().NotBeEmpty();
            context.PrimaryEntityId.Should().NotBe(Guid.Empty);
        }

        [TestCaseSource("providers"), Category("Provider")]
        public void Get_OriginalProvider(IServiceProvider provider)
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
        public void Get_TracingService(IServiceProvider provider)
        {
            // Act
            var service = provider.GetService(typeof(ITracingService));

            // Assert
            service.GetType().Should().BeAssignableTo<ITracingService>();
        }

        [TestCaseSource("providers"), Category("Provider"), Category("Factory")]
        public void Get_WrappedFactory(IServiceProvider provider)
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
            var context = Substitute.For<IPluginExecutionContext>();
            context.MessageName.Returns("ValidMessageName");
            context.PrimaryEntityName.Returns("ValidEntityName");
            context.PrimaryEntityId.Returns(Guid.NewGuid());
            context.ParentContext.Returns(new CuteContext());

            var originalProvider = Substitute.For<IServiceProvider>();
            originalProvider.GetService(typeof(IPluginExecutionContext)).Returns(context);
            originalProvider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());

            return new CuteProvider(originalProvider);
        }

        private static IServiceProvider CreateNoInputProvider()
        {
            var provider = new CuteProvider();

            provider.Context.MessageName = "ValidMessageName";
            provider.Context.PrimaryEntityName = "ValidEntityName";
            provider.Context.PrimaryEntityId = Guid.NewGuid();

            return provider;
        }

        private static IServiceProvider CreateSerializedInputProvider()
        {
            var provider = CreateBareInputProvider();
            return new CuteProvider(((CuteProvider)provider).ToBase64String());
        }

        private static IServiceProvider CreateTransparentInputProvider()
        {
            var provider = new CuteProvider(Substitute.For<IOrganizationService>());

            provider.Context.MessageName = "ValidMessageName";
            provider.Context.PrimaryEntityName = "ValidEntityName";
            provider.Context.PrimaryEntityId = Guid.NewGuid();

            return provider;
        }

        private static IServiceProvider CreateWrappedInputProvider()
        {
            var provider = CreateBareInputProvider();
            return new CuteProvider(provider);
        }

        #endregion Private Methods
    }
}