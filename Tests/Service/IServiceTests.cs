namespace Cinteros.Unit.Test.Extensions.Tests.Service
{
    using Cinteros.Unit.Test.Extensions.Core;
    using Microsoft.Xrm.Sdk;

    public interface IServiceTests
    {
        #region Public Properties

        CuteProvider Provider
        {
            get;
        }

        IOrganizationService Service
        {
            get;
        }

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