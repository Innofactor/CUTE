namespace Cinteros.Unit.Test.Extensions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    public class SpecialTrustSandbox : MarshalByRefObject
    {
        private Dictionary<string, string> assemblies;
        #region Public Methods

        public bool GetAssembly(string assemblyPath)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            try
            {
                Assembly.LoadFile(assemblyPath).GetTypes();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = this.assemblies.Where(x => x.Key == args.Name).FirstOrDefault();

            return Assembly.LoadFile(name.Value);
        }

        #endregion Public Methods

        public Exception Execute(/*IXunitTestCase testCase, IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource*/)
        {
            //var tcs = new TaskCompletionSource<RunSummary>();

            //try
            //{
            //    var testCaseTask = testCase.RunAsync(
            //        diagnosticMessageSink,
            //        messageBus,
            //        constructorArguments,
            //        aggregator,
            //        cancellationTokenSource);

            //    tcs.SetResult(testCaseTask.Result);
            //}
            //catch (Exception e)
            //{
            //    tcs.SetException(e);
            //}

            //return tcs;

            return new Exception();
        }

        internal void GetAssemblies(Dictionary<string, string> assemblies)
        {
            this.assemblies = assemblies;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            try
            {
                foreach (var assembly in assemblies)
                {
                    Assembly.LoadFile(assembly.Value).GetTypes();
                }
            }
            catch (Exception)
            {
            }
        }

        //internal RunSummary Execute(Check check)
        //{
        //    //try
        //    //{
        //    //    var testCaseTask = check.TestCase.RunAsync(
        //    //        check.DiagnosticMessageSink,
        //    //        check.MessageBus,
        //    //        check.ConstructorArguments,
        //    //        check.Aggregator,
        //    //        check.CancellationTokenSource);

        //    //    check.TaskCompletitionSource.SetResult(testCaseTask.Result);
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    check.TaskCompletitionSource.SetException(e);
        //    //}

        //    try
        //    {
        //        var testCaseTask = check.TestCase.RunAsync(check.DiagnosticMessageSink, check.MessageBus, check.ConstructorArguments, check.Aggregator, check.CancellationTokenSource);

        //        return testCaseTask.Result;
        //    }
        //    catch (Exception e)
        //    {
        //        // check.TaskCompletitionSource.SetException(e);
        //        throw;
        //    }
        //}

        internal Exception Execute(SpecialTrustTest test)
        {
            // Forward this data from the outside application domain
            //Console.SetOut(testMethodInfo.OutputStream);
            //Console.SetError(testMethodInfo.ErrorStream);

            Type typeUnderTest = test.Method.DeclaringType;

            object instance = Activator.CreateInstance(typeUnderTest);

            Exception exceptionCaught = null;

            try
            {
                // mark this test as being in the app domain.  As soon as we're done, we're going to tear down
                // the app domain, so there is no need to set this back to false. 
                //AppDomainRunner.IsInTestAppDomain = true;

                //foreach (var setupMethod in testMethodInfo.Methods.SetupMethods)
                //{
                //    setupMethod.Invoke(instance, null);
                //}

                test.Method.Invoke(instance, new object[] { });

                //foreach (var teardownMethod in testMethodInfo.Methods.TeardownMethods)
                //{
                //    teardownMethod.Invoke(instance, null);
                //}
            }
            catch (TargetInvocationException e)
            {
                // TODO when moving to .NET 4.5, find out if using ExceptionDispatchInfo.Capture helps at all. 
                exceptionCaught = e.InnerException;
            }

            return exceptionCaught;
        }
    }
}