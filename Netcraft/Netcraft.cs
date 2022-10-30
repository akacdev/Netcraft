using Netcraft.Modules;
using System;
using System.Net;
using System.Net.Http;

namespace Netcraft
{
    /// <summary>
    /// The primary class for interacting with the Netcraft API.
    /// </summary>
    public class NetcraftClient
    {
        public const int Version = 3;
        public static readonly string BaseUrl = $"https://report.netcraft.com/api/v{Version}/";
        public static readonly Uri BaseUri = new(BaseUrl);
        public static readonly TimeSpan Timeout = TimeSpan.FromMinutes(5);

        /// <summary>
        /// The primary HTTP client for sending API requests.
        /// </summary>
        private readonly HttpClient Client = new(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.All,
            AllowAutoRedirect = false
        }) { BaseAddress = BaseUri };

        /// <summary>
        /// Create a new instance of the API client.
        /// </summary>
        public NetcraftClient()
        {
            Client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("Netcraft C# Client - actually-akac/Netcraft");
            Client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            Client.DefaultRequestHeaders.Accept.ParseAdd("*/*");

            Report = new(Client);
            Submission = new(Client);
            Misc = new(Client);
        }

        /// <summary>
        /// Report malicious URLs, emails or mistakes.
        /// </summary>
        public readonly ReportModule Report;

        /// <summary>
        /// Get details about submissions, analysis, fetch metadata or report classificiaton issues.
        /// </summary>
        public readonly SubmissionModule Submission;

        /// <summary>
        /// Miscellaneous features that don't belong to any other module.
        /// </summary>
        public readonly MiscModule Misc;
    }
}