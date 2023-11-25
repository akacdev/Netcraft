using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netcraft
{
    internal class Constants
    {
        /// <summary>
        /// The version of the API to send requests to.
        /// </summary>
        public const int Version = 3;
        /// <summary>
        /// The base URI to send requests to.
        /// </summary>
        public static readonly Uri BaseUri = new($"https://report.netcraft.com/api/v{Version}/");
        /// <summary>
        /// The preferred HTTP request version to use.
        /// </summary>
        public static readonly Version HttpVersion = new(2, 0);
        /// <summary>
        /// The <c>User-Agent</c> header value to send along requests.
        /// </summary>
        public const string UserAgent = "Netcraft C# Client - actually-akac/Netcraft";
        /// <summary>
        /// The maximum string length when displaying a preview of a response body.
        /// </summary>
        public const int PreviewMaxLength = 500;
        /// <summary>
        /// A string used to identify successfull reports.
        /// </summary>
        public const string SuccessReportMessage = "Successfully reported";
        /// <summary>
        /// A string used to identify successfull mistake reports.
        /// </summary>
        public const string SuccessMistakeReportMessage = "Successfully reported mistake";
        /// <summary>
        /// JSON serializer options used to serialize enums.
        /// </summary>
        public static readonly JsonSerializerOptions EnumOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(new EnumNamingPolicy())
            }
        };
    }
}