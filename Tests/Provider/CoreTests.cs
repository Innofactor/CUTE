namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

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
            Assert.True(this.Provider.IsOnline);
        }

        public virtual void Get_Context()
        {
            // Act
            var context = this.Provider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.IsInstanceOf<IPluginExecutionContext>(context);
            Assert.IsNotAssignableFrom<CuteContext>(context);
        }

        public virtual void Get_OriginalProvider()
        {
            // Assert
            Assert.IsNotInstanceOf<CuteProvider>(this.Provider.Original);
        }

        public virtual void Get_TracingService()
        {
            // Act
            var service = this.Provider.GetService(typeof(ITracingService));

            // Assert
            Assert.IsInstanceOf<ITracingService>(service);
        }

        public virtual void Get_WrappedFactory()
        {
            // Act
            var factory = Provider.GetService(typeof(IOrganizationServiceFactory));

            // Assert
            Assert.IsInstanceOf<IOrganizationServiceFactory>(factory);
            Assert.IsAssignableFrom<CuteFactory>(factory);
        }

        #endregion Public Methods
    }
}