namespace Cinteros.Unit.Testing.Extensions.Tests
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using FluentAssertions;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class ServiceTestCases
    {
        #region Protected Fields

        protected static Guid expectedResultCreate;

        protected static OrganizationResponse expectedResultExecute;

        protected static Entity expectedResultRetrieve;

        protected static EntityCollection expectedResultRetrieveMultiple;

        #endregion Protected Fields

        #region Private Fields

        private IOrganizationService[] services = new IOrganizationService[]
        {
            CreateBareInputService(),
            CreateNoInputService(),
            CreateSerializedInputService(),
            CreateStandaloneInputService(),
            CreateTransparentInputService(),
            CreateWrappedInputService()
        };

        private object[][] serviceTypes = new object[][]
        {
            new object[] { CreateBareInputService(), InstanceType.BareInput },
            new object[] { CreateNoInputService(), InstanceType.NoInput },
            new object[] { CreateSerializedInputService(), InstanceType.SerializedInput },
            new object[] { CreateStandaloneInputService(), InstanceType.StandaloneInput },
            new object[] { CreateTransparentInputService(), InstanceType.TransparentInput },
            new object[] { CreateWrappedInputService(), InstanceType.WrappedInput }
        };

        #endregion Private Fields

        #region Public Constructors

        static ServiceTestCases()
        {
            expectedResultCreate = Guid.NewGuid();

            expectedResultRetrieve = new Entity()
            {
                Id = Guid.NewGuid()
            };

            expectedResultRetrieveMultiple = new EntityCollection();
            expectedResultRetrieveMultiple.Entities.Add(new Entity());
            expectedResultRetrieveMultiple.Entities.Add(new Entity());
            expectedResultRetrieveMultiple.Entities.Add(new Entity());
            expectedResultRetrieveMultiple.Entities.Add(new Entity());
            expectedResultRetrieveMultiple.Entities.Add(new Entity());

            expectedResultExecute = new OrganizationResponse()
            {
                ResponseName = "Test"
            };
        }

        #endregion Public Constructors

        #region Public Methods

        [TestCaseSource("serviceTypes"), Category("Service")]
        public void Check_Service_Type(IOrganizationService service, InstanceType expected)
        {
            // Assert
            ((CuteService)service).Provider.Type.Should().Be(expected);
        }

        [TestCaseSource("services"), Category("Service")]
        public void Invoke_Associate(IOrganizationService service)
        {
            // Act
            service.Associate(string.Empty, Guid.Empty, new Relationship(), new EntityReferenceCollection());
        }

        [TestCaseSource("services"), Category("Service")]
        public void Invoke_Create_Check_Cache(IOrganizationService service)
        {
            // Arrange
            Action fail = () =>
            {
                service.Create(new Entity("fail"));
            };

            // Act
            var result = service.Create(new Entity());

            // Assert
            result.GetType().Should().Be<Guid>(because: "message `Create` should always return Id of record created");
            result.Should().NotBe(Guid.Empty, because: "record's Id cannot be empty Guid");
            result.Should().Be(expectedResultCreate);

            fail.ShouldThrow<InvalidPluginExecutionException>();

            ((CuteService)service).Provider.Calls.Where(x => x.Message == MessageName.Create).Count().Should().Be(2, because: "two `Create` call was executed already");
        }

        [TestCaseSource("services"), Category("Service")]
        public void Invoke_Delete(IOrganizationService service)
        {
            // Arrange
            Action fail = () =>
            {
                service.Delete("fail", Guid.Empty);
            };

            // Act
            service.Delete(string.Empty, Guid.Empty);

            // Assert
            fail.ShouldThrow<InvalidPluginExecutionException>();

            ((CuteService)service).Provider.Calls.Where(x => x.Message == MessageName.Delete).Count().Should().Be(2, because: "two `Delete` call was executed already");
        }

        [TestCaseSource("services"), Category("Service")]
        public virtual void Invoke_Disassociate(IOrganizationService service)
        {
            // Act
            service.Disassociate(string.Empty, Guid.Empty, new Relationship(), new EntityReferenceCollection());
        }

        [TestCaseSource("services"), Category("Service")]
        public virtual void Invoke_Execute_Check_Cache(IOrganizationService service)
        {
            // Act
            var result = service.Execute(new OrganizationRequest());

            // Assert
            result.Should().NotBe(null);
            result.GetType().Should().Be<OrganizationResponse>();
            result.ShouldBeEquivalentTo(expectedResultExecute, options => options.Excluding(x => x.ExtensionData));

            ((CuteService)service).Provider.Calls.Where(x => x.Message == MessageName.Execute).Count().Should().Be(1, because: "Should be only one cached Execute call");
        }

        [TestCaseSource("services"), Category("Service")]
        public virtual void Invoke_Retrieve_Check_Cache(IOrganizationService service)
        {
            // Act
            var result = service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());

            // Assert
            result.Should().NotBe(null);
            result.ShouldBeEquivalentTo(expectedResultRetrieve, options => options.Excluding(x => x.ExtensionData));

            ((CuteService)service).Provider.Calls.Where(x => x.Message == MessageName.Retrieve).Count().Should().Be(1);
        }

        [TestCaseSource("services"), Category("Service")]
        public virtual void Invoke_RetrieveMultiple_Check_Cache(IOrganizationService service)
        {
            // Act
            var result = service.RetrieveMultiple(new QueryExpression());

            // Assert
            result.Should().NotBe(null);
            result.GetType().Should().Be<EntityCollection>();
            result.ShouldBeEquivalentTo<EntityCollection>(ServiceTestCases.expectedResultRetrieveMultiple, options => (options).Excluding(x => x.SelectedMemberPath.EndsWith("ExtensionData")));

            ((CuteService)service).Provider.Calls.Where(x => x.Message == MessageName.RetrieveMultiple).Count().Should().Be(1);
        }

        [TestCaseSource("services"), Category("Service")]
        public void Invoke_Update(IOrganizationService service)
        {
            // Act
            service.Update(new Entity());
        }

        #endregion Public Methods

        #region Private Methods

        private static IOrganizationService CreateBareInputService()
        {
            var originalProvider = Substitute.For<IServiceProvider>();
            var originalFactory = Substitute.For<IOrganizationServiceFactory>();
            var originalService = Substitute.For<IOrganizationService>();

            originalService.Create(Arg.Is<Entity>(x => x.LogicalName != "fail")).Returns(ServiceTestCases.expectedResultCreate);
            originalService.Create(Arg.Is<Entity>(x => x.LogicalName == "fail")).Returns(x => { throw new InvalidPluginExecutionException(); });

            originalService.When(x => x.Delete(Arg.Is<string>(y => y == "fail"), Arg.Any<Guid>())).Do(x => { throw new InvalidPluginExecutionException(); });

            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(ServiceTestCases.expectedResultRetrieve);
            originalService.RetrieveMultiple(Arg.Any<QueryBase>()).Returns(ServiceTestCases.expectedResultRetrieveMultiple);
            originalService.Execute(Arg.Any<OrganizationRequest>()).Returns(ServiceTestCases.expectedResultExecute);

            originalFactory.CreateOrganizationService(Arg.Any<Guid?>()).Returns(originalService);

            originalProvider.GetService(typeof(IOrganizationServiceFactory)).Returns(originalFactory);

            return ((IOrganizationServiceFactory)new CuteProvider(originalProvider).GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        private static IOrganizationService CreateNoInputService()
        {
            // Creating provider from scratch
            var originalProvider = new CuteProvider();

            originalProvider.Calls.Add(new CuteCall(MessageName.Create, new[] { new Entity() }, ServiceTestCases.expectedResultCreate));
            originalProvider.Calls.Add(new CuteCall(MessageName.Create, new[] { new Entity("fail") }, new InvalidPluginExecutionException()));

            originalProvider.Calls.Add(new CuteCall(MessageName.Delete, new object[] { string.Empty, Guid.Empty }));
            originalProvider.Calls.Add(new CuteCall(MessageName.Delete, new object[] { "fail", Guid.Empty }, new InvalidPluginExecutionException()));

            originalProvider.Calls.Add(new CuteCall(MessageName.Retrieve, new object[] { string.Empty, Guid.Empty, new ColumnSet() }, ServiceTestCases.expectedResultRetrieve));
            originalProvider.Calls.Add(new CuteCall(MessageName.RetrieveMultiple, new object[] { new QueryExpression() }, ServiceTestCases.expectedResultRetrieveMultiple));
            originalProvider.Calls.Add(new CuteCall(MessageName.Execute, new object[] { new OrganizationRequest() }, ServiceTestCases.expectedResultExecute));

            return ((IOrganizationServiceFactory)originalProvider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        private static IOrganizationService CreateSerializedInputService()
        {
            var service = ServiceTestCases.CreateBareInputService();

            // Calling methods to fill calls stack
            service.Create(new Entity());
            try
            {
                service.Create(new Entity("fail"));
            }
            catch (Exception)
            {
            }

            service.Delete(string.Empty, Guid.Empty);
            try
            {
                service.Delete("fail", Guid.Empty);
            }
            catch (Exception)
            {
            }

            service.Retrieve(string.Empty, Guid.Empty, new ColumnSet());
            service.RetrieveMultiple(new QueryExpression());
            service.Execute(new OrganizationRequest());

            // Recreating provider from serialized one
            var provider = new CuteProvider(((CuteService)service).Provider.ToBase64String());
            return ((IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        private static IOrganizationService CreateStandaloneInputService()
        {
            var originalService = Substitute.For<IOrganizationService>();

            originalService.Create(Arg.Is<Entity>(x => x.LogicalName != "fail")).Returns(ServiceTestCases.expectedResultCreate);
            originalService.Create(Arg.Is<Entity>(x => x.LogicalName == "fail")).Returns(x => { throw new InvalidPluginExecutionException(); });

            originalService.When(x => x.Delete(Arg.Is<string>(y => y == "fail"), Arg.Any<Guid>())).Do(x => { throw new InvalidPluginExecutionException(); });

            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(ServiceTestCases.expectedResultRetrieve);
            originalService.RetrieveMultiple(Arg.Any<QueryBase>()).Returns(ServiceTestCases.expectedResultRetrieveMultiple);
            originalService.Execute(Arg.Any<OrganizationRequest>()).Returns(ServiceTestCases.expectedResultExecute);

            return new CuteService(originalService);
        }

        private static IOrganizationService CreateTransparentInputService()
        {
            var originalService = Substitute.For<IOrganizationService>();

            originalService.Create(Arg.Is<Entity>(x => x.LogicalName != "fail")).Returns(ServiceTestCases.expectedResultCreate);
            originalService.Create(Arg.Is<Entity>(x => x.LogicalName == "fail")).Returns(x => { throw new InvalidPluginExecutionException(); });

            originalService.When(x => x.Delete(Arg.Is<string>(y => y == "fail"), Arg.Any<Guid>())).Do(x => { throw new InvalidPluginExecutionException(); });

            originalService.Retrieve(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<ColumnSet>()).Returns(ServiceTestCases.expectedResultRetrieve);
            originalService.RetrieveMultiple(Arg.Any<QueryBase>()).Returns(ServiceTestCases.expectedResultRetrieveMultiple);
            originalService.Execute(Arg.Any<OrganizationRequest>()).Returns(ServiceTestCases.expectedResultExecute);

            return ((IOrganizationServiceFactory)new CuteProvider(originalService).GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        private static IOrganizationService CreateWrappedInputService()
        {
            var originalService = ServiceTestCases.CreateBareInputService();

            var provider = new CuteProvider(((CuteService)originalService).Provider);
            return ((IOrganizationServiceFactory)provider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(Guid.Empty);
        }

        #endregion Private Methods
    }
}