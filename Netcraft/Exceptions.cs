using System;
using Netcraft.Entities;

namespace Netcraft
{
    /// <summary>
    /// A custom AbuseIPDB API exception with an array of <see cref="NetcraftError"/> containing all parsed API errors.
    /// </summary>
    public class NetcraftException : Exception
    {
        /// <summary>
        /// The HTTP method that caused this exception.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// The requested path or URL that caused this exception.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An array of user-friendly API errors.
        /// </summary>
        public NetcraftError[] Errors { get; set; } = Array.Empty<NetcraftError>();

        public NetcraftException(string message, string method = null, string url = null) : base(message)
        {
            Method = method;
            Url = url;
        }

        public NetcraftException(string message, NetcraftError[] errors, string method = null, string url = null) : base(message)
        {
            Errors = errors;
            Method = method;
            Url = url;
        }
    }
}