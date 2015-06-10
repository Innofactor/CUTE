namespace Cinteros.Unit.Testing.Extensions.Tests.Service
{
    using Cinteros.Unit.Testing.Extensions.Core;
    using Microsoft.Xrm.Sdk;

    internal interface ICoreTests
    {
        #region Public Properties

        CuteProvider Provider { get; }

        IOrganizationService Service { get; }

        #endregion Public Properties

        #region Public Methods

        void Invoke_Associate();

        void Invoke_Create_Check_Cache();

        void Invoke_Delete();

        void Invoke_Disassociate();

        void Invoke_Execute_Check_Cache();

        void Invoke_Retrieve_Check_Cache();

        void Invoke_RetrieveMultiple_Check_Cache();

        void Invoke_Update();

        #endregion Public Methods
    }
}