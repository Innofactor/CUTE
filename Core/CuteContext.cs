namespace Cinteros.Unit.Testing.Extensions.Core
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An implementation of IPluginExecutionContext designed to be serializable
    /// </summary>
    [DataContract]
    public class CuteContext : IPluginExecutionContext
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CuteContext"/> class.
        /// </summary>
        public CuteContext()
        {
            InputParameters = new ParameterCollection();
            OutputParameters = new ParameterCollection();
            PreEntityImages = new EntityImageCollection();
            PostEntityImages = new EntityImageCollection();
        }

        #endregion Public Constructors

        #region Private Constructors

        private CuteContext(IPluginExecutionContext context)
        {
            if (context != null)
            {
                BusinessUnitId = context.BusinessUnitId;
                CorrelationId = context.CorrelationId;
                Depth = context.Depth;
                InitiatingUserId = context.InitiatingUserId;
                InputParameters = context.InputParameters;
                IsExecutingOffline = context.IsExecutingOffline;
                IsInTransaction = context.IsInTransaction;
                IsOfflinePlayback = context.IsOfflinePlayback;
                IsolationMode = context.IsolationMode;
                MessageName = context.MessageName;
                Mode = context.Mode;
                OperationCreatedOn = context.OperationCreatedOn;
                OperationId = context.OperationId;
                OrganizationId = context.OrganizationId;
                OrganizationName = context.OrganizationName;
                OutputParameters = context.OutputParameters;
                OwningExtension = context.OwningExtension;
                PostEntityImages = context.PostEntityImages;
                PreEntityImages = context.PreEntityImages;
                PrimaryEntityId = context.PrimaryEntityId;
                PrimaryEntityName = context.PrimaryEntityName;
                RequestId = context.RequestId;
                SecondaryEntityName = context.SecondaryEntityName;
                SharedVariables = context.SharedVariables;
                Stage = context.Stage;
                UserId = context.UserId;
            }
        }

        #endregion Private Constructors

        #region Public Properties

        [DataMember]
        public Guid BusinessUnitId
        {
            get;
            set;
        }

        [DataMember]
        public Guid CorrelationId
        {
            get;
            set;
        }

        [DataMember]
        public int Depth
        {
            get;
            set;
        }

        [DataMember]
        public Guid InitiatingUserId
        {
            get;
            set;
        }

        [DataMember]
        public ParameterCollection InputParameters
        {
            get;
            set;
        }

        [DataMember]
        public bool IsExecutingOffline
        {
            get;
            set;
        }

        [DataMember]
        public bool IsInTransaction
        {
            get;
            set;
        }

        [DataMember]
        public bool IsOfflinePlayback
        {
            get;
            set;
        }

        [DataMember]
        public int IsolationMode
        {
            get;
            set;
        }

        [DataMember]
        public string MessageName
        {
            get;
            set;
        }

        [DataMember]
        public int Mode
        {
            get;
            set;
        }

        [DataMember]
        public DateTime OperationCreatedOn
        {
            get;
            set;
        }

        [DataMember]
        public Guid OperationId
        {
            get;
            set;
        }

        [DataMember]
        public Guid OrganizationId
        {
            get;
            set;
        }

        [DataMember]
        public string OrganizationName
        {
            get;
            set;
        }

        [DataMember]
        public ParameterCollection OutputParameters
        {
            get;
            set;
        }

        [DataMember]
        public EntityReference OwningExtension
        {
            get;
            set;
        }

        [DataMember]
        public CuteContext Parent
        {
            get;
            set;
        }

        [DataMember]
        public IPluginExecutionContext ParentContext
        {
            get;
            set;
        }

        [DataMember]
        public EntityImageCollection PostEntityImages
        {
            get;
            set;
        }

        [DataMember]
        public EntityImageCollection PreEntityImages
        {
            get;
            set;
        }

        [DataMember]
        public Guid PrimaryEntityId
        {
            get;
            set;
        }

        [DataMember]
        public string PrimaryEntityName
        {
            get;
            set;
        }

        [DataMember]
        public Guid? RequestId
        {
            get;
            set;
        }

        [DataMember]
        public string SecondaryEntityName
        {
            get;
            set;
        }

        [DataMember]
        public ParameterCollection SharedVariables
        {
            get;
            set;
        }

        [DataMember]
        public int Stage
        {
            get;
            set;
        }

        [DataMember]
        public Guid UserId
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public static CuteContext Copy(IPluginExecutionContext context)
        {
            if (context == null)
            {
                return null;
            }

            var copy = new CuteContext(context);
            if (context.ParentContext != null)
            {
                copy.Parent = CuteContext.Copy(context.ParentContext);
            }

            return copy;
        }

        #endregion Public Methods
    }
}