namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class TransparentInputTests : CoreTests, ICoreTests
    {
        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Transparent Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        [SetUp]
        public void Setup()
        {
            this.Provider = new CuteProvider(Substitute.For<IOrganizationService>());

            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }
    }
}
