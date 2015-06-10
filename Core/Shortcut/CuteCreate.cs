namespace Cinteros.Unit.Testing.Extensions.Core.Shortcut
{
    using System;
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;

    public class CuteCreate : CuteCall
    {
        #region Public Constructors

        public CuteCreate(Entity entity)
            : base(MessageName.Create, new[] { entity })
        {
        }

        public CuteCreate(Entity entity, Guid id)
            : base(MessageName.Create, new[] { entity }, id)
        {
        }

        #endregion Public Constructors
    }
}