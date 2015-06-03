namespace Cinteros.Unit.Test.Extensions.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cinteros.Unit.Test.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// Service that replaces requests with their cached versions
    /// </summary>
    public class CuteService : IOrganizationService
    {
        #region Private Fields

        private CuteProvider provider;
        private IOrganizationService service;

        #endregion Private Fields

        #region Public Constructors

        public CuteService(CuteProvider provider, Guid? userId)
        {
            this.provider = provider;
            this.UserId = userId;

            // Sign that provider was deserialized
            if (this.provider.IsOnline)
            {
                var factory = (IOrganizationServiceFactory)provider.Original.GetService(typeof(IOrganizationServiceFactory));
                
                this.service = factory.CreateOrganizationService(userId);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public Guid? UserId
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="relationship"></param>
        /// <param name="relatedEntities"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            if (this.provider.IsOnline)
            {
                this.service.Associate(entityName, entityId, relationship, relatedEntities);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Guid Create(Entity entity)
        {
            var call = new CuteCall(MessageName.Create, new object[] { entity }); 

            if (this.provider.IsOnline)
            {
                call.Output = this.service.Create(entity);

                this.provider.Calls.Add(call);

                return (Guid)call.Output;
            }
            else
            {
                return this.provider.Calls.Where(x => x.Equals(call)).Select(x => (Guid)x.Output).FirstOrDefault();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(string entityName, Guid id)
        {
            if (this.provider.IsOnline)
            {
                this.service.Delete(entityName, id);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="entityId"></param>
        /// <param name="relationship"></param>
        /// <param name="relatedEntities"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            if (this.provider.IsOnline)
            {
                this.service.Disassociate(entityName, entityId, relationship, relatedEntities);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrganizationResponse Execute(OrganizationRequest request)
        {
            if (this.provider.IsOnline)
            {
                var result = this.service.Execute(request);

                this.provider.Calls.Add(new CuteCall(MessageName.Execute, new[] { request }, result));
                
                return result;
            }
            else
            {
                var call = new CuteCall(MessageName.Execute, new[] { request });

                return this.provider.Calls.Where(x => x.Equals(call)).Select(x => (OrganizationResponse)x.Output).FirstOrDefault();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <param name="columnSet"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            var call = new CuteCall(MessageName.Retrieve, new object[] { entityName, id, columnSet }); 

            if (this.provider.IsOnline)
            {
                call.Output = this.service.Retrieve(entityName, id, columnSet);

                this.provider.Calls.Add(call);

                return (Entity)call.Output;
            }
            else
            {
                return this.provider.Calls.Where(x => x.Equals(call)).Select(x => (Entity)x.Output).FirstOrDefault();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            var call = new CuteCall(MessageName.RetrieveMultiple, new[] { query });

            if (this.provider.IsOnline)
            {
                call.Output = this.service.RetrieveMultiple(query);

                this.provider.Calls.Add(call);

                return (EntityCollection)call.Output;
            }
            else
            {
                return this.provider.Calls.Where(x => x.Equals(call)).Select(x => (EntityCollection)x.Output).FirstOrDefault();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(Entity entity)
        {
            if (this.provider.IsOnline)
            {
                this.service.Update(entity);
            }
        }

        #endregion Public Methods
    }
}