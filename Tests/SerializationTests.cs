namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Xml;

    public class SerializationTests
    {
        #region Public Methods

        [Test]
        [Category("Serialization")]
        public void Serialize_Deserialize()
        {
            // Arrange
            var inputContext = Substitute.For<IPluginExecutionContext>();
            inputContext.ParentContext.Returns(new CuteContext());
            inputContext.PrimaryEntityName.Returns("account");
            inputContext.MessageName.Returns("Create");

            var provider = Substitute.For<IServiceProvider>();
            provider.GetService(typeof(IPluginExecutionContext)).Returns(inputContext);

            var inputProvider = new CuteProvider(provider);
            inputProvider.Calls.Add(new CuteCall("Create"));
            inputProvider.Calls.Add(new CuteCall("Update"));

            // Act
            var outputProvider = new CuteProvider(inputProvider.ToBase64String());
            var outputContext = (IPluginExecutionContext)outputProvider.GetService(typeof(IPluginExecutionContext));

            // Assert
            outputProvider.Should().NotBeNull();
            outputProvider.Should().BeAssignableTo<CuteProvider>();

            outputProvider.Calls.Should().NotBeNull();
            outputProvider.Calls.Count.Should().Be(inputProvider.Calls.Count);

            outputProvider.Context.Should().NotBeNull();
            outputContext.Should().NotBeNull();

            outputProvider.Context.Should().BeAssignableTo<CuteContext>();
            outputContext.Should().BeAssignableTo<CuteContext>();

            outputProvider.Context.PrimaryEntityName.Should().Be("account");
            outputProvider.Context.MessageName.Should().Be("Create");

            outputContext.PrimaryEntityName.Should().Be("account");
            outputContext.MessageName.Should().Be("Create");

            outputProvider.Original.Should().BeNull();
        }

        [Test]
        [Category("Serialization")]
        public void Serialize_To_Base64String()
        {
            // Arrange
            var provider = new CuteProvider(Substitute.For<IServiceProvider>());

            // Act
            var result = provider.ToBase64String();

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