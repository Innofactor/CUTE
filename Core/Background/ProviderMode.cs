namespace Cinteros.Unit.Testing.Extensions.Core.Background
{
    internal enum ProviderMode
    {
        /// <summary>
        /// Provider that was created from no input, locally. Usually happens when running local
        /// unit tests.
        /// </summary>
        NoInput,

        /// <summary>
        /// Provider that was created directly from IService provider taken from MS CRM itself.
        /// Usually happens when running on server.
        /// </summary>
        BareInput,

        /// <summary>
        /// Provider that created from already wrapped provider. Usually happens when running local
        /// unit tests, and provider becomes wpapped twice: 1) when it was created initially, and 2)
        /// when plugin wrapps it for having capturing output
        /// </summary>
        WrappedInput,

        /// <summary>
        /// Provider that was created from serialized data. Usually happen when running local unit tests.
        /// </summary>
        SerializedInput,

        /// <summary>
        /// Provider that was created using live connection to MS CRM database. Usually happens when
        /// running local unit tests.
        /// </summary>
        TransmittedInput
    }
}