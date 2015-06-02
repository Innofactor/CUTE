namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Xunit;

    public abstract class ProviderTests
    {
        #region Protected Fields

        protected CuteProvider Provider;

        #endregion Protected Fields

        #region Public Methods

        public abstract void Check_Online_Status();

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