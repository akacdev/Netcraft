using System;
using System.Net.Http;
using System.Net;
using Netcraft.Entities;

namespace Netcraft
{
    /// <summary>
    /// A custom AbuseIPDB API exception with an array of <see cref="NetcraftError"/> containing all parsed API errors.
    /// </summary>
    public class NetcraftException : Exception
    {
        /// <summary>
        /// The HTTP request method used that triggered this exception.
        /// </summary>
        public HttpMethod Method { get; set; }
        /// <summary>
        /// The HTTP path used that triggered this exception.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The HTTP status code used that triggered this exception.
        /// </summary>
        public HttpStatusCode? StatusCode { get; set; }

        /// <summary>
        /// An array of user-friendly API errors.
        /// </summary>
        public NetcraftError[] Errors { get; set; } = Array.Empty<NetcraftError>();

        public NetcraftException(string message) : base(message) { }
        public NetcraftException(string message, HttpResponseMessage res) : base(message)
        {
            Method = res.RequestMessage.Method;
            Path = res.RequestMessage.RequestUri.AbsolutePath;
            StatusCode = res.StatusCode;
        }
        public NetcraftException(string message, NetcraftError[] errors, HttpResponseMessage res) : base(message)
        {
            Errors = errors;
            Method = res.RequestMessage.Method;
            Path = res.RequestMessage.RequestUri.AbsolutePath;
            StatusCode = res.StatusCode;
        }
    }
}