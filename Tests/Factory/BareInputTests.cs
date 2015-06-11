namespace Cinteros.Unit.Testing.Extensions.Tests.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    class BareInputTests : CoreTests, ICoreTests
    {
        [Test]
        [Category("Factory")]
        [Category("Bare Input")]
        public override void Get_OrganizationService()
        {
            base.Get_OrganizationService();
        }
    }
}
