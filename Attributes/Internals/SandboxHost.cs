namespace Cinteros.Unit.Testing.Extensions.Attributes.Internals
{
    using System;
    using System.Reflection;

    internal class SandboxHost : MarshalByRefObject
    {
        //#region Private Fields

        //private Dictionary<string, string> assemblies;

        //#endregion Private Fields

        //public bool GetAssembly(string assemblyPath)
        //{
        //    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

        // try { Assembly.LoadFile(assemblyPath).GetTypes();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        #region Internal Methods

        internal Exception Execute(SandboxTest test)
        {
            // Forward this data from the outside application domain
            //Console.SetOut(testMethodInfo.OutputStream);
            //Console.SetError(testMethodInfo.ErrorStream);

            Type typeUnderTest = test.MainMethod.DeclaringType;

            object instance = Activator.CreateInstance(typeUnderTest);

            Exception exceptionCaught = null;

            try
            {
                // mark this test as being in the app domain.  As soon as we're done, we're going to tear down
                // the app domain, so there is no need to set this back to false.
                //AppDomainRunner.IsInTestAppDomain = true;

                foreach (var method in test.SetupMethods)
                {
                    method.Invoke(instance, null);
                }

                test.MainMethod.Invoke(instance, new object[] { });

                foreach (var method in test.TeardownMethods)
                {
                    method.Invoke(instance, null);
                }
            }
            catch (TargetInvocationException e)
            {
                // TODO when moving to .NET 4.5, find out if using ExceptionDispatchInfo.Capture
                // helps at all.
                exceptionCaught = e.InnerException;
            }

            return exceptionCaught;
        }

        #endregion Internal Methods

        //internal void GetAssemblies(Dictionary<string, string> assemblies)
        //{
        //    this.assemblies = assemblies;

        // AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

        //    try
        //    {
        //        foreach (var assembly in assemblies)
        //        {
        //            Assembly.LoadFile(assembly.Value).GetTypes();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //#region Private Methods

        //private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    var name = this.assemblies.Where(x => x.Key == args.Name).FirstOrDefault();

        //    return Assembly.LoadFile(name.Value);
        //}

        //#endregion Private Methods
    }
}