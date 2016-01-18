namespace Cinteros.Unit.Testing.Extensions.Core
{
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Xml;

    [DataContract]
    public class CuteProvider : IServiceProvider
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CuteProvider"/> class.
        /// </summary>
        /// <param name="data">Serialized data that used to re-create Provider</param>
        public CuteProvider(string data)
            : this()
        {
            var saved = Serialization.Inflate<CuteProvider>(data, new CuteProvider().Types);
            Context = saved.Context;
            Calls = saved.Calls;
            Type = InstanceType.SerializedInput;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CuteProvider"/> class.
        /// </summary>
        /// <param name="Provider">Provider that will be acting as a back-end for current one</param>
        public CuteProvider(IServiceProvider provider)
            : this()
        {
            if (provider.GetType() == typeof(CuteProvider))
            {
                Original = ((CuteProvider)provider).Original;
                Type = InstanceType.WrappedInput;
            }
            else
            {
                Original = provider;
                Type = InstanceType.BareInput;
            }

            Context = CuteContext.Copy((IPluginExecutionContext)this.Original.GetService(typeof(IPluginExecutionContext)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CuteProvider"/> class.
        /// </summary>
        public CuteProvider()
        {
            Context = new CuteContext();
            Calls = new Collection<CuteCall>();
            Type = InstanceType.NoInput;

            Types = new Type[]
                {
                    typeof(object),
                    typeof(Entity),
                    typeof(EntityCollection),
                    typeof(QueryExpression),
                    typeof(ColumnSet),
                    typeof(OrganizationRequest),
                    typeof(OrganizationResponse),
                    typeof(InvalidPluginExecutionException),
                    typeof(OperationStatus)
                };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CuteProvider"/> class.
        /// </summary>
        /// <param name="proxy"></param>
        public CuteProvider(IOrganizationService proxy)
            : this()
        {
            Proxy = proxy;
            Type = InstanceType.TransparentInput;
        }

        #endregion Public Constructors

        #region Public Properties

        [DataMember]
        public Collection<CuteCall> Calls
        {
            get;
            private set;
        }

        [DataMember]
        public CuteContext Context
        {
            get;
            private set;
        }

        public IServiceProvider Original
        {
            get;
            private set;
        }

        public IOrganizationService Proxy
        {
            get;
            private set;
        }

        public InstanceType Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// Types used by <see cref="CuteProvider"/> and all subsidiary objects
        /// </summary>
        public Type[] Types
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates instance of service of type set by <paramref name="serviceType"/>
        /// </summary>
        /// <param name="serviceType">Type of the service to spawn</param>
        /// <returns>Service instance</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IPluginExecutionContext))
            {
                if (Original == null)
                {
                    // Original IServiceProvider is not available, so use cached version of the object
                    return this.Context;
                }
            }

            if (serviceType == typeof(IOrganizationServiceFactory))
            {
                // Always returning wrapped version of the IOrganizationServiceFactory
                return new CuteFactory(this);
            }

            if (serviceType == typeof(ITracingService))
            {
                if (Original == null)
                {
                    // Original IServiceProvider is not available, so use cached version of the object
                    return new CuteTracing();
                }
            }

            if (Original != null)
            {
                return Original.GetService(serviceType);
            }

            throw new NotImplementedException(string.Format("Behavior for service of type '{0}' is not defined yet.", serviceType.ToString()));
        }

        /// <summary>
        /// Serializes object into deflated and compressed string
        /// </summary>
        /// <returns></returns>
        public string ToBase64String()
        {
            return Serialization.Deflate(this, Types);
        }

        public override string ToString()
        {
            return string.Format("{0}Provider", this.Type.ToString());
        }

        /// <summary>
        /// Serializes object to XML representation
        /// </summary>
        /// <returns></returns>
        public XmlDocument ToXml()
        {
            var document = new XmlDocument();
            document.LoadXml(Serialization.Serialize(this, Types));

            return document;
        }

        #endregion Public Methods
    }
}