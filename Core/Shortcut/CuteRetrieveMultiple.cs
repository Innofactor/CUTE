namespace Cinteros.Unit.Testing.Extensions.Core.Shortcut
{
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    public class CuteRetrieveMultiple : CuteCall
    {
        #region Public Constructors

        public CuteRetrieveMultiple(QueryExpression query)
            : base(MessageName.RetrieveMultiple, new[] { query })
        {
        }

        public CuteRetrieveMultiple(QueryExpression query, EntityCollection collection)
            : base(MessageName.RetrieveMultiple, new[] { query }, collection)
        {
        }

        #endregion Public Constructors
    }
}