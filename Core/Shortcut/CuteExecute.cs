namespace Cinteros.Unit.Testing.Extensions.Core.Shortcut
{
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;

    public class CuteExecute : CuteCall
    {
        #region Public Constructors

        public CuteExecute(OrganizationRequest request)
            : base(MessageName.Execute, new object[] { request })
        {
        }

        public CuteExecute(OrganizationRequest request, OrganizationResponse response)
            : base(MessageName.Execute, new object[] { request }, response)
        {
        }

        #endregion Public Constructors
    }
}