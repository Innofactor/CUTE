namespace Cinteros.Unit.Test.Extensions.Tests.Background
{
    using System;
    using System.Xml;
    using Cinteros.Unit.Test.Extensions.Core;
    using Cinteros.Unit.Test.Extensions.Helpers;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class SerializationTests
    {
        #region Public Methods

        [Test, SpecialTrust]
        [Category("Serialization")]
        public void Partial_Trust()
        {
        }

        [Test]
        [Category("Serialization")]
        public void Serialize_Deserialize()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());

            // Assert
            Assert.IsInstanceOf<CuteProvider>(outputProvider);
        }

        [Test]
        [Category("Serialization")]
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
            Assert.AreEqual(inputProvider.Calls.Count, outputProvider.Calls.Count);
        }

        [Test]
        [Category("Serialization")]
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

            Assert.IsInstanceOf<CuteContext>(outputProvider.Context);
            Assert.IsInstanceOf<CuteContext>(outputContext);

            Assert.AreEqual("account", outputProvider.Context.PrimaryEntityName);
            Assert.AreEqual("Create", outputProvider.Context.MessageName);

            Assert.AreEqual("account", outputContext.PrimaryEntityName);
            Assert.AreEqual("Create", outputContext.MessageName);
        }

        [Test]
        [Category("Serialization")]
        public void Serialize_Deserialize_Check_Original_Provider()
        {
            // Arrange
            var inputProvider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToString());

            // Assert
            Assert.Null(outputProvider.Original);
        }

        [Test]
        [Category("Serialization")]
        public void Serialize_To_String()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToString();

            // Assert
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        [Category("Serialization")]
        public void Serialize_To_XML()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToXml();

            // Assert
            Assert.IsInstanceOf<XmlDocument>(result);
        }

        #endregion Public Methods
    }
}