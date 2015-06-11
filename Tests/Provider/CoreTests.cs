namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;
    using FluentAssertions;

    public class CoreTests : ICoreTests
    {
        #region Public Constructors

        #endregion Public Constructors

        #region Public Properties

        public CuteProvider Provider
        {
            get;
            protected set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Setup()
        {
            // Arrange
            this.Provider = new CuteProvider(Substitute.For<IServiceProvider>());
            this.Provider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());
            this.Provider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
        }

        public virtual void Check_Online_Status()
        {
            // Assert
            this.Provider.IsOnline.Should().BeTrue();
        }

        public virtual void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            context.Should().BeAssignableTo<IPluginExecutionContext>();
            context.GetType().Should().NotBe<CuteContext>();
        }

        public virtual void Get_OriginalProvider()
        {
            // Assert
            this.Provider.Original.GetType().Should().NotBe<CuteProvider>();
        }

        public virtual void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            service.GetType().Should().BeAssignableTo<ITracingService>();
        }

        public virtual void Get_WrappedFactory()
        {
            // Act
            var factory = Provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            factory.GetType().Should().BeAssignableTo<IOrganizationServiceFactory>();
            factory.GetType().Should().BeAssignableTo<CuteFactory>();
        }

        #endregion Public Methods
    }
}