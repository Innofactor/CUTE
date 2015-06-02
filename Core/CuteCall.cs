namespace Cinteros.Unit.Test.Extensions.Core
{
    using System.Runtime.Serialization;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// Item representing call to <see cref="IOrganizationService"/> and respective response
    /// </summary>
    [DataContract]
    public class CuteCall
    {
        #region Public Constructors

        public CuteCall(string messageName)
        {
            this.MessageName = messageName;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets collection of call's input parametes
        /// </summary>
        [DataMember]
        public object[] Input
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of the call's message
        /// </summary>
        [DataMember]
        public string MessageName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets call's output parameter
        /// </summary>
        [DataMember]
        public object Output
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Compares two objects using Knuth hashes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var call = (CuteCall)obj;

            if (call.MessageName != this.MessageName)
            {
                return false;
            }

            if (this.MessageName == "RetrieveMultiple")
            {
                if (this.Input[0] is QueryExpression)
                {
                    var thisOne = (QueryExpression)this.Input[0];
                    var thatOne = (QueryExpression)call.Input[0];

                    thisOne.PageInfo = null;
                    thatOne.PageInfo = null;

                    return Serialization.Hash<QueryExpression>(thatOne) == Serialization.Hash<QueryExpression>(thisOne);
                }
            }

            return Serialization.Hash<object[]>(call.Input) == Serialization.Hash<object[]>(this.Input);
        }

        /// <summary>
        /// Calculates Knuth hash for given object
        /// </summary>
        /// <returns></returns>
        public new ulong GetHashCode()
        {
            return Serialization.Hash<object[]>(this.Input);
        }

        #endregion Public Methods
    }
}