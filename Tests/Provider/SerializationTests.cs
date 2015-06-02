namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using System.Xml;
    using Cinteros.Unit.Test.Extensions.Core;
    using NSubstitute;
    using Xunit;

    public class SerializationTests
    {
        #region Public Methods

        [Fact(DisplayName = "Serialize + Deserialize")]
        [Trait("Module", "Serialization")]
        public void Test_Serialize_Deserialize()
        {
            // Arrange
            var inputProvider = new WrappedProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new WrappedProvider(inputProvider.ToString());

            // Assert
            Assert.True(outputProvider is WrappedProvider);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Calls")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        public void Test_Serialize_Deserialize_Check_Calls()
        {
            // Arrange
            var inputProvider = new WrappedProvider(Substitute.For<IServiceProvider>());
            inputProvider.Calls.Add(new CuteCall("Create"));
            inputProvider.Calls.Add(new CuteCall("Update"));

            // Act
            var outputProvider = new WrappedProvider(inputProvider.ToString());

            // Assert
            Assert.NotNull(outputProvider.Calls);
            Assert.Equal(inputProvider.Calls.Count, outputProvider.Calls.Count);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Context")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        public void Test_Serialize_Deserialize_Check_Context()
        {
            // Arrange
            var inputProvider = new WrappedProvider(Substitute.For<IServiceProvider>());
            inputProvider.Context.PrimaryEntityName = "account";
            inputProvider.Context.MessageName = "Create";

            // Act
            var outputProvider = new WrappedProvider(inputProvider.ToString());

            // Assert
            Assert.NotNull(outputProvider.Context);
            Assert.Equal("account", outputProvider.Context.PrimaryEntityName);
            Assert.Equal("Create", outputProvider.Context.MessageName);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Original Provider")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        public void Test_Serialize_Deserialize_Check_Original_Provider()
        {
            // Arrange
            var inputProvider = new WrappedProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new WrappedProvider(inputProvider.ToString());

            // Assert
            Assert.Equal(null, outputProvider.Original);
        }

        [Fact(DisplayName = "Serialize To String")]
        [Trait("Module", "Serialization")]
        public void Test_Serialize_To_String()
        {
            // Arrange
            var provider = new WrappedProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToString();

            // Assert
            Assert.True(result is string);
        }

        [Fact(DisplayName = "Serialize To XML")]
        [Trait("Module", "Serialization")]
        public void Test_Serialize_To_XML()
        {
            // Arrange
            var provider = new WrappedProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToXml();

            // Assert
            Assert.True(result is XmlDocument);
        }

        #endregion Public Methods
    }
}