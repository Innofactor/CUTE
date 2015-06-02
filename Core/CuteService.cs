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
            throw new NotImplementedException();

            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Guid Create(Entity entity)
        {
            throw new NotImplementedException();

            return this.provider.Calls.Where(x =>
            {
                return x.MessageName == MessageName.Create && (Entity)x.Input[0] == entity;
            }).Select(x => (Guid)x.Output).FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(string entityName, Guid id)
        {
            throw new NotImplementedException();

            return;
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
            throw new NotImplementedException();

            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrganizationResponse Execute(OrganizationRequest request)
        {
            throw new NotImplementedException();

            return new OrganizationResponse();
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
            throw new NotImplementedException();

            return this.provider.Calls.Where(x =>
            {
                return (x.MessageName == MessageName.Retrieve) && (Guid)x.Input[0] == id && (ColumnSet)x.Input[1] == columnSet;
            }).Select(x => (Entity)x.Output).FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            throw new NotImplementedException();

            var call = new CuteCall(MessageName.RetrieveMultiple)
            {
                Input = new[] { query }
            };

            return this.provider.Calls.Where(x => x.Equals(call)).Select(x => (EntityCollection)x.Output).FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(Entity entity)
        {
            throw new NotImplementedException();

            return;
        }

        #endregion Public Methods
    }
}