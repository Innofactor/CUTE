namespace Cinteros.Unit.Testing.Extensions.Tests.Provider
{
    using NUnit.Framework;

    public class BareInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public BareInputTests()
            : base()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Provider"), Category("Bare Input"), Category("Context")]
        public new void Get_Context()
        {
            base.Get_Context();
        }

        [Test]
        [Category("Provider"), Category("Bare Input")]
        public new void Get_OriginalProvider()
        {
            base.Get_OriginalProvider();
        }

        [Test]
        [Category("Provider"), Category("Bare Input")]
        public new void Get_TracingService()
        {
            base.Get_TracingService();
        }

        [Test]
        [Category("Provider"), Category("Bare Input"), Category("Factory")]
        public new void Get_WrappedFactory()
        {
            base.Get_WrappedFactory();
        }

        #endregion Public Methods
    }
}