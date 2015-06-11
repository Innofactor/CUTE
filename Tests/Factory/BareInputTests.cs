namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using NUnit.Framework;

    internal class BareInputTests : CoreTests, ICoreTests
    {
        #region Public Methods

        [Test]
        [Category("Factory"), Category("Bare Input")]
        public override void Get_OrganizationService()
        {
            base.Get_OrganizationService();
        }

        #endregion Public Methods
    }
}