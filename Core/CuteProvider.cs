namespace Cinteros.Unit.Test.Extensions.Core
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Xml;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    [DataContract]
    public class WrappedProvider : IServiceProvider
    {
        #region Public Constructors

        public WrappedProvider(string data)
            : this()
        {
            var saved = Serialization.Inflate<WrappedProvider>(data, WrappedProvider.Types);
            this.Context = saved.Context;
            this.Calls = saved.Calls;
        }

        public WrappedProvider(IServiceProvider provider)
            : this()
        {
            if (provider.GetType() == typeof(WrappedProvider))
            {
                this.Original = ((WrappedProvider)provider).Original;
            }
            else
            {
                this.Original = provider;
            }
        }

        #endregion Public Constructors

        #region Private Constructors

        private WrappedProvider()
        {
            this.Context = new CuteContext();
            this.Calls = new Collection<CuteCall>();
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Types used by <see cref="InputDataContainer"/> and all subsidiary objects
        /// </summary>
        public static Type[] Types
        {
            get
            {
                return new[] { typeof(object), typeof(Entity), typeof(EntityCollection), typeof(QueryExpression) };
            }
        }

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

        #endregion Public Properties

        #region Public Methods

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IPluginExecutionContext))
            {
                return new CuteContext();
            }

            if (serviceType == typeof(IOrganizationServiceFactory))
            {
                return new CuteFactory();
            }

            return this.Original.GetService(serviceType);
        }

        /// <summary>
        /// Serializes object into deflated and compressed string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Serialization.Deflate<WrappedProvider>(this, WrappedProvider.Types);
        }

        /// <summary>
        /// Serializes object to XML representation
        /// </summary>
        /// <returns></returns>
        public XmlDocument ToXml()
        {
            var document = new XmlDocument();
            document.LoadXml(Serialization.Serialize<WrappedProvider>(this, WrappedProvider.Types));

            return document;
        }

        #endregion Public Methods
    }
}