namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;
    using Xunit;

    public class WrappedInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public WrappedInputTests()
            : base()
        {
            this.Provider = new CuteProvider(this.Provider);
            this.Service = ((IOrganizationServiceFactory)this.Provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Test]
        [Category("Service")]
        [Category("Wrapped Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}