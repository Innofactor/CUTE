namespace Cinteros.Unit.Testing.Extensions.Attributes.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    internal class SandboxTest : MarshalByRefObject
    {
        #region Public Constructors

        public SandboxTest(TestDetails testDetails)
        {
            this.MainMethod = testDetails.Method;

            this.SetupMethods = this.GetMethodsWith<TestFixtureSetUpAttribute>(testDetails.Method.DeclaringType);
            this.SetupMethods.AddRange(this.GetMethodsWith<SetUpAttribute>(testDetails.Method.DeclaringType));

            this.SetupMethods.Reverse();

            this.TeardownMethods = this.GetMethodsWith<TestFixtureTearDownAttribute>(testDetails.Method.DeclaringType);
            this.TeardownMethods.AddRange(this.GetMethodsWith<TearDownAttribute>(testDetails.Method.DeclaringType));
        }

        #endregion Public Constructors

        #region Public Properties

        public MethodInfo MainMethod
        {
            get;
            set;
        }

        public List<MethodInfo> SetupMethods
        {
            get;
            set;
        }

        public List<MethodInfo> TeardownMethods
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Private Methods

        private List<MethodInfo> GetMethodsWith<T>(Type type)
            where T : Attribute
        {
            return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => x.GetParameters().Length == 0 && x.GetCustomAttribute<T>(false) != null).ToList();
        }

        #endregion Private Methods
    }
}