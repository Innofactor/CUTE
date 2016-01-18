namespace Cinteros.Unit.Testing.Extensions.Core
{
    using Cinteros.Unit.Testing.Extensions.Core.Background;
    using Microsoft.Xrm.Sdk.Query;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Item representing call to <see cref="IOrganizationService"/> and respective response
    /// </summary>
    [DataContract]
    public class CuteCall
    {
        #region Public Constructors

        public CuteCall(string messageName)
        {
            Message = messageName;
        }

        public CuteCall(string messageName, object[] input)
        {
            Message = messageName;
            Input = input;
        }

        public CuteCall(string messageName, object[] input, object output)
        {
            Message = messageName;
            Input = input;
            Output = output;
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
        public string Message
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

            if (call.Message != this.Message)
            {
                return false;
            }

            switch (this.Message)
            {
                case MessageName.Retrieve:
                    {
                        if ((string)Input[0] != (string)call.Input[0])
                        {
                            return false;
                        }

                        if ((Guid)Input[1] != (Guid)call.Input[1])
                        {
                            return false;
                        }

                        var thisOne = (ColumnSet)Input[2];
                        var thatOne = (ColumnSet)call.Input[2];

                        thisOne.ExtensionData = null;
                        thatOne.ExtensionData = null;

                        return Serialization.Hash<ColumnSet>(thatOne) == Serialization.Hash<ColumnSet>(thisOne);
                    }

                case MessageName.RetrieveMultiple:
                    {
                        if (Input[0] is QueryExpression)
                        {
                            var thisOne = (QueryExpression)Input[0];
                            var thatOne = (QueryExpression)call.Input[0];

                            thisOne.PageInfo = null;
                            thatOne.PageInfo = null;

                            return Serialization.Hash<QueryExpression>(thatOne) == Serialization.Hash<QueryExpression>(thisOne);
                        }
                    }

                    break;
            }

            var types = new CuteProvider().Types;

            return Serialization.Hash(call.Input, types) == Serialization.Hash(Input, types);
        }

        /// <summary>
        /// Calculates Knuth hash for given object
        /// </summary>
        /// <returns></returns>
        public new ulong GetHashCode()
        {
            return Serialization.Hash(Input);
        }

        #endregion Public Methods
    }
}