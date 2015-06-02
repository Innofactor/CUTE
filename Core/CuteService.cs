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

        private List<CuteCall> _calls;
        private CuteProvider provider;
        public Guid? UserId
        {
            get;
            private set;
        }

        public CuteService(CuteProvider provider, Guid? userId)
        {
            // TODO: Complete member initialization
            this.provider = provider;
            this.UserId = userId;
        }

        #endregion Private Fields

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
            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Guid Create(Entity entity)
        {
            return this._calls.Where(x =>
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
            return;
        }

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public OrganizationResponse Execute(OrganizationRequest request)
        {
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
            return this._calls.Where(x =>
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
            var call = new CuteCall(MessageName.RetrieveMultiple)
            {
                Input = new[] { query }
            };

            return this._calls.Where(x => x.Equals(call)).Select(x => (EntityCollection)x.Output).FirstOrDefault();
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(Entity entity)
        {
            return;
        }

        #endregion Public Methods
    }
}