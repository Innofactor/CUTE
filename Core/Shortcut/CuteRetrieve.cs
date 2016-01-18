namespace Cinteros.Unit.Testing.Extensions.Core.Shortcut
{
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;

    public class CuteRetrieve : CuteCall
    {
        #region Public Constructors

        public CuteRetrieve(string entityName, Guid id, ColumnSet columnSet)
            : base(MessageName.Retrieve, new object[] { entityName, id, columnSet })
        {
        }

        public CuteRetrieve(string entityName, Guid id, ColumnSet columnSet, Entity entity)
            : base(MessageName.Retrieve, new object[] { entityName, id, columnSet }, entity)
        {
        }

        #endregion Public Constructors
    }
}