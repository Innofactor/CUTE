namespace Cinteros.Unit.Test.Extensions.Tests.Provider
{
    using System;
    using System.Xml;
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using Xunit;

    public class SerializationTests
    {
        #region Public Methods

        [Fact(DisplayName = "Serialize + Deserialize")]
        [Trait("Module", "Serialization")]
        public void Serialize_Deserialize()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());

            // Assert
            Assert.IsType<CuteProvider>(outputProvider);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Calls")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Call")]
        public void Serialize_Deserialize_Check_Calls()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());
            inputProvider.Calls.Add(new CuteCall("Create"));
            inputProvider.Calls.Add(new CuteCall("Update"));

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());

            // Assert
            Assert.NotNull(outputProvider.Calls);
            Assert.Equal(inputProvider.Calls.Count, outputProvider.Calls.Count);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Context")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        [Trait("Module", "Context")]
        public void Serialize_Deserialize_Check_Context()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());
            inputProvider.Context.PrimaryEntityName = "account";
            inputProvider.Context.MessageName = "Create";

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());
            var outputContext = (IPluginExecutionContext)outputProvider.GetService(typeof(IPluginExecutionContext));

            // Assert
            Assert.NotNull(outputProvider.Context);
            Assert.NotNull(outputContext);
            
            Assert.IsType<CuteContext>(outputProvider.Context);
            Assert.IsType<CuteContext>(outputContext);

            Assert.Equal("account", outputProvider.Context.PrimaryEntityName);
            Assert.Equal("Create", outputProvider.Context.MessageName);

            Assert.Equal("account", outputContext.PrimaryEntityName);
            Assert.Equal("Create", outputContext.MessageName);
        }

        [Fact(DisplayName = "Serialize + Deserialize & Check Original Provider")]
        [Trait("Module", "Serialization")]
        [Trait("Module", "Provider")]
        public void Serialize_Deserialize_Check_Original_Provider()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());

            // Assert
            Assert.Null(outputProvider.Original);
        }

        [Fact(DisplayName = "Serialize To String")]
        [Trait("Module", "Serialization")]
        public void Serialize_To_String()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToString();

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact(DisplayName = "Serialize To XML")]
        [Trait("Module", "Serialization")]
        public void Serialize_To_XML()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToXml();

            // Assert
            Assert.IsType<XmlDocument>(result);
        }

        #endregion Public Methods
    }
}