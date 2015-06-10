namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class CoreTests
    {
        #region Public Constructors

        public CoreTests()
        {
            // Arrange
            this.Provider = new CuteProvider(Substitute.For<IServiceProvider>());
            this.Provider.GetService(typeof(IPluginExecutionContext)).Returns(Substitute.For<IPluginExecutionContext>());
            this.Provider.GetService(typeof(ITracingService)).Returns(Substitute.For<ITracingService>());
        }

        #endregion Public Constructors

        #region Public Properties

        public CuteProvider Provider
        {
            get;
            protected set;
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Check_Online_Status()
        {
            // Assert
            Assert.True(this.Provider.IsOnline);
        }

        public virtual void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsAssignableFrom<IPluginExecutionContext>(context);
            Assert.IsNotType<CuteContext>(context);
        }

        public virtual void Get_OriginalProvider()
        {
            // Assert
            Assert.IsNotType<CuteProvider>(this.Provider.Original);
        }

        public virtual void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsAssignableFrom<ITracingService>(service);
        }

        public virtual void Get_WrappedFactory()
        {
            // Act
            var factory = Provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsAssignableFrom<IOrganizationServiceFactory>(factory);
            Assert.IsType<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}