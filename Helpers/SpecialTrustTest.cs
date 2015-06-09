namespace Cinteros.Unit.Test.Extensions.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    public class SpecialTrustTest : MarshalByRefObject
    {
        public SpecialTrustTest(TestDetails testDetails)
        {
            this.Method = testDetails.Method;
        }

        public MethodInfo Method 
        { 
            get; 
            set; 
        }
    }
}
