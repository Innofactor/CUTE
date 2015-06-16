namespace Cinteros.Unit.Testing.Extensions.Attributes
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security;
    using System.Security.Policy;
    using Cinteros.Unit.Testing.Extensions.Attributes.Internals;
    using Microsoft.Xrm.Sdk;
    using NUnit.Framework;

    [AttributeUsage(AttributeTargets.Method)]
    public class RequiresIsolationAttribute : TestActionAttribute
    {
        #region Public Methods

        public override void BeforeTest(TestDetails testDetails)
        {
            var evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Intranet));
            var permissions = SecurityManager.GetStandardSandbox(evidence);
            // var permissions = new PermissionSet(PermissionState.Unrestricted);

            var setup = new AppDomainSetup
            {
                ApplicationBase = Path.GetDirectoryName(Assembly.GetAssembly(typeof(SandboxHost)).Location)
            };

            var fullTrustAssemblies = new StrongName[]
            {
                typeof(SandboxHost).Assembly.Evidence.GetHostEvidence<StrongName>(),
                typeof(Entity).Assembly.Evidence.GetHostEvidence<StrongName>(),
                testDetails.Method.DeclaringType.Assembly.Evidence.GetHostEvidence<StrongName>()
            };

            var domain = AppDomain.CreateDomain(testDetails.FullName, evidence, setup, permissions, fullTrustAssemblies);

            var type = typeof(SandboxHost);

            var handle = Activator.CreateInstanceFrom(domain, type.Assembly.ManifestModule.FullyQualifiedName, type.FullName);

            var sandbox = (SandboxHost)handle.Unwrap();

            var exception = sandbox.Execute(new SandboxTest(testDetails));

            if (exception == null)
            {
                Assert.Pass();
            }

            throw exception;
        }

        #endregion Public Methods
    }
}