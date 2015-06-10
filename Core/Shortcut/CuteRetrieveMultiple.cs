namespace Cinteros.Unit.Testing.Extensions.Core.Shortcut
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinteros.Unit.Testing.Extensions.Core.Background;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

    public class CuteRetrieveMultiple : CuteCall
    {
        public CuteRetrieveMultiple(QueryExpression query)
            : base(MessageName.RetrieveMultiple, new[] { query })
        {
        }

        public CuteRetrieveMultiple(QueryExpression query, EntityCollection collection)
            : base(MessageName.RetrieveMultiple, new[] { query }, collection)
        {
        }
    }
}
