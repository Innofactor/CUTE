﻿namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using NUnit.Framework;

    public class StandaloneInputTests : CoreTests, ICoreTests
    {
        #region Public Constructors

        public StandaloneInputTests()
        {
            var originalService = Substitute.For<IOrganizationService>();

            originalService.Create(Arg.Is<Entity>(x => x.LogicalName != "fail")).Returns(this.expectedResultCreate);
            originalService.Create(Arg.Is<Entity>(x => x.LogicalName == "fail")).Returns(x => { throw new InvalidPluginExecutionException(); });
            
            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(this.expectedResultRetrieve);
            originalService.RetrieveMultiple(Arg.Any<QueryBase>()).Returns(this.expectedResultRetrieveMultiple);
            originalService.Execute(Arg.Any<OrganizationRequest>()).Returns(this.expectedResultExecute);

            this.Service = new CuteService(originalService);
        }

        #endregion Public Constructors

        #region Public Methods

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Associate()
        {
            base.Invoke_Associate();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Create_Check_Cache()
        {
            base.Invoke_Create_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Delete()
        {
            base.Invoke_Delete();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Disassociate()
        {
            base.Invoke_Disassociate();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Execute_Check_Cache()
        {
            base.Invoke_Execute_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Retrieve_Check_Cache()
        {
            base.Invoke_Retrieve_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_RetrieveMultiple_Check_Cache()
        {
            base.Invoke_RetrieveMultiple_Check_Cache();
        }

        [Test]
        [Category("Service"), Category("Standalone Input")]
        public override void Invoke_Update()
        {
            base.Invoke_Update();
        }

        #endregion Public Methods
    }
}