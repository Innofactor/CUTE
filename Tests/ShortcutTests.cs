namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Attributes;
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Cinteros.Unit.Testing.Extensions.Core.Shortcut;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NUnit.Framework;

    public class ShortcutTests
    {
        #region Private Fields

        private CuteProvider provider;

        #endregion Private Fields

        #region Public Methods

        [Test, SpecialTrust]
        [Category("Call")]
        public void Create_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteCreate(new Entity());
            var nakedCall = new CuteCall(MessageName.Create, new[] { new Entity() }, null);
            this.provider.Calls.Add(dressedCall);

            // Assert
            Assert.AreEqual(MessageName.Create, dressedCall.Message);
            Assert.AreEqual(1, this.provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Test]
        [Category("Call")]
        public void Execute_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteExecute(new OrganizationRequest());
            var nakedCall = new CuteCall(MessageName.Execute, new object[] { new OrganizationRequest() }, null);
            provider.Calls.Add(dressedCall);

            // Assert
            Assert.AreEqual(MessageName.Execute, dressedCall.Message);
            Assert.AreEqual(1, provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Test]
        [Category("Call")]
        public void Retrieve_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteRetrieve(string.Empty, Guid.Empty, new ColumnSet());
            var nakedCall = new CuteCall(MessageName.Retrieve, new object[] { string.Empty, Guid.Empty, new ColumnSet() }, null);
            this.provider.Calls.Add(dressedCall);

            // Assert
            Assert.AreEqual(MessageName.Retrieve, dressedCall.Message);
            Assert.AreEqual(1, this.provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Test]
        [Category("Call")]
        public void RetrieveMultiple_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteRetrieveMultiple(new QueryExpression());
            var nakedCall = new CuteCall(MessageName.RetrieveMultiple, new object[] { new QueryExpression() }, null);
            provider.Calls.Add(dressedCall);

            // Assert
            Assert.AreEqual(MessageName.RetrieveMultiple, dressedCall.Message);
            Assert.AreEqual(1, provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.provider = new CuteProvider();
        }

        #endregion Public Methods
    }
}