namespace Cinteros.Unit.Test.Extensions.Core
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.Serialization;

    /// <summary>
    /// Set of methods allowing various serialization approaches
    /// </summary>
    public class Serialization
    {
        #region Public Methods

        /// <summary>
        /// Serializes object and pack them using gzip
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="serializationObject"/></typeparam>
        /// <param name="serializationObject">Object to serialize</param>
        /// <param name="knownTypes">Array of known types will be serialized</param>
        /// <returns>Base64 representation of deflated serialization string</returns>
        public static string Deflate<T>(T serializationObject, Type[] knownTypes = null)
        {
            knownTypes = knownTypes ?? new[] { typeof(object) };

            var contextFormatter = new DataContractSerializer(typeof(T), knownTypes);

            using (var input = new MemoryStream())
            {
                // Write the XML to the stream
                contextFormatter.WriteObject(input, serializationObject);
                input.Position = 0;

                using (var output = new MemoryStream())
                {
                    using (var deflate = new DeflateStream(output, CompressionMode.Compress))
                    {
                        input.CopyTo(deflate);
                    }

                    return Convert.ToBase64String(output.ToArray(), Base64FormattingOptions.InsertLineBreaks);
                }
            }
        }

        /// <summary>
        /// Expands serialized object and deserializes it into object
        /// </summary>
        /// <typeparam name="T">Type of result to get</typeparam>
        /// <param name="serializationString"></param>
        /// <param name="knownTypes">Array of known types will be deserialized</param>
        /// <returns>Object representation of gziped serialization string</returns>
        public static T Deserialize<T>(string serializationString, Type[] knownTypes = null)
        {
            knownTypes = knownTypes ?? new[] { typeof(object) };

            var contextFormatter = new DataContractSerializer(typeof(T), knownTypes);

            using (var input = new MemoryStream())
            {
                using (var medium = new StreamWriter(input))
                {
                    medium.Write(serializationString);

                    return (T)contextFormatter.ReadObject(input);
                }
            }
        }

        /// <summary>
        /// Calculating Knuth hash of the given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializationObject"></param>
        /// <param name="knownTypes">Array of known types will be serialized</param>
        /// <returns></returns>
        public static UInt64 Hash<T>(T serializationObject, Type[] knownTypes = null)
        {
            knownTypes = knownTypes ?? new[] { typeof(object) };

            var serializationString = Serialization.Serialize<T>(serializationObject, knownTypes);

            UInt64 hashedValue = 3074457345618258791ul;

            for (int i = 0; i < serializationString.Length; i++)
            {
                hashedValue += serializationString[i];
                hashedValue *= 3074457345618258799ul;
            }

            return hashedValue;
        }

        /// <summary>
        /// Expands serialized object and deserializes it into object
        /// </summary>
        /// <typeparam name="T">Type of result to get</typeparam>
        /// <param name="serializationString"></param>
        /// <param name="knownTypes">Array of known types will be deserialized</param>
        /// <returns>Object representation of deflated serialization string</returns>
        public static T Inflate<T>(string serializationString, Type[] knownTypes = null)
        {
            knownTypes = knownTypes ?? new[] { typeof(object) };

            var contextFormatter = new DataContractSerializer(typeof(T), knownTypes);

            using (var input = new MemoryStream())
            {
                var data = Convert.FromBase64String(serializationString);
                input.Write(data, 0, data.Length);
                input.Position = 0;

                using (var output = new MemoryStream())
                {
                    using (var deflate = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        deflate.CopyTo(output);
                    }
                    output.Position = 0;

                    return (T)contextFormatter.ReadObject(output);
                }
            }
        }

        /// <summary>
        /// Serializes given object to XML representation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializationObject"></param>
        /// <param name="knownTypes">Array of known types will be serialized</param>
        /// <returns></returns>
        public static string Serialize<T>(T serializationObject, Type[] knownTypes = null)
        {
            knownTypes = knownTypes ?? new[] { typeof(object) };

            var contextFormatter = new DataContractSerializer(typeof(T), knownTypes);

            using (var stream = new MemoryStream())
            {
                // Write the XML to the stream
                contextFormatter.WriteObject(stream, serializationObject);

                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        #endregion Public Methods
    }
}