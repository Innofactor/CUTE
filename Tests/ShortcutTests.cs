namespace Cinteros.Unit.Test.Extensions.Tests
{
    using System;
    using System.Linq;
    using Cinteros.Unit.Test.Extensions.Core;
    using Cinteros.Unit.Test.Extensions.Core.Background;
    using Cinteros.Unit.Test.Extensions.Core.Shortcut;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using Xunit;

    public class ShortcutTests
    {
        private CuteProvider provider;
        public ShortcutTests()
        {
            // Arrange
            this.provider = new CuteProvider();
        }

        #region Public Methods

        [Fact(DisplayName = "Create Call Shortcut")]
        [Trait("Module", "Call")]
        public void Create_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteCreate(new Entity());
            var nakedCall = new CuteCall(MessageName.Create, new[] { new Entity() }, null);
            this.provider.Calls.Add(dressedCall);

            // Assert
            Assert.Equal(MessageName.Create, dressedCall.Message);
            Assert.Equal(1, this.provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Fact(DisplayName = "Retrieve Call Shortcut")]
        [Trait("Module", "Call")]
        public void Retrieve_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteRetrieve(string.Empty, Guid.Empty, new ColumnSet());
            var nakedCall = new CuteCall(MessageName.Retrieve, new object[] { string.Empty, Guid.Empty, new ColumnSet() }, null);
            this.provider.Calls.Add(dressedCall);

            // Assert
            Assert.Equal(MessageName.Retrieve, dressedCall.Message);
            Assert.Equal(1, this.provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Fact(DisplayName = "RetrieveMultiple Call Shortcut")]
        [Trait("Module", "Call")]
        public void RetrieveMultiple_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteRetrieveMultiple(new QueryExpression());
            var nakedCall = new CuteCall(MessageName.RetrieveMultiple, new object[] { new QueryExpression() }, null);
            provider.Calls.Add(dressedCall);

            // Assert
            Assert.Equal(MessageName.RetrieveMultiple, dressedCall.Message);
            Assert.Equal(1, provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        [Fact(DisplayName = "Execute Call Shortcut")]
        [Trait("Module", "Call")]
        public void Execute_Call_Shortcut()
        {
            // Act
            var dressedCall = new CuteExecute(new OrganizationRequest());
            var nakedCall = new CuteCall(MessageName.Execute, new object[] { new OrganizationRequest() }, null);
            provider.Calls.Add(dressedCall);

            // Assert
            Assert.Equal(MessageName.Execute, dressedCall.Message);
            Assert.Equal(1, provider.Calls.Count);
            Assert.True(dressedCall.Equals(nakedCall));
        }

        #endregion Public Methods
    }
}