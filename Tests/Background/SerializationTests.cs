namespace Cinteros.Unit.Testing.Extensions.Tests.Background
{
    using System;
    using System.Xml;
    using Cinteros.Unit.Testing.Extensions.Attributes;
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;

    public class SerializationTests
    {
        #region Public Methods

        [Test]
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
            outputProvider.Should().BeAssignableTo<CuteProvider>();
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
            outputProvider.Calls.Should().NotBeNull();
            outputProvider.Calls.Count.Should().Be(inputProvider.Calls.Count);
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
            outputProvider.Context.Should().NotBeNull();
            outputContext.Should().NotBeNull();

            outputProvider.Context.Should().BeAssignableTo<CuteContext>();
            outputContext.Should().BeAssignableTo<CuteContext>();

            outputProvider.Context.PrimaryEntityName.Should().Be("account");
            outputProvider.Context.MessageName.Should().Be("Create");

            outputContext.PrimaryEntityName.Should().Be("account");
            outputContext.MessageName.Should().Be("Create");
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
            outputProvider.Original.Should().BeNull();
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
            result.Should().BeAssignableTo<string>();
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
            result.Should().BeAssignableTo<XmlDocument>();
        }

        #endregion Public Methods
    }
}