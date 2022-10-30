using System.Text.Json.Serialization;

namespace Netcraft.Entities
{
    /// <summary>
    /// The result of a reporting operation.
    /// </summary>
    public class ReportResult
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("uuid")]
        public string UUID { get; set; }
    }

    /// <summary>
    /// Parameters passed into the API for reporting malicious URLs.
    /// </summary>
    public class UrlsReportParameters
    {
        /// <summary>
        /// Your email for receiving confirmation and classification updates.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The reason for reporting.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The URLs to report.
        /// </summary>
        [JsonPropertyName("urls")]
        public UrlReportParmeters[] Urls { get; set; }

        /// <summary>
        /// The source UUID. Only include if you were provided with one.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }

        /// <summary>
        /// Enables reporting without creating submissions. This option is only available for a subset of reporters.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("no_submission")]
        public bool? NoSubmission { get; set; }
    }

    /// <summary>
    /// Parameters passed into the API for reporting malicious emails.
    /// </summary>
    public class EmailReportParameters
    {
        /// <summary>
        /// Your email for receiving confirmation and classification updates.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        ///     One of:<br/>
        ///     1) Plain text string of the malicious email in <b>MIME format</b><br/>
        ///     2) If the password parameter is provided: A base64 encoded <b>AES-256-CBC</b> encrypted email in <b>MIME format</b>. Max message size is 20MiB.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The password if the email is encrypted.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class UrlReportParmeters
    {
        /// <summary>
        /// The country that this malicious URL is known to be accessible from, to aid analysis.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("url")]
        public string Value { get; set; }
    }

    /// <summary>
    /// Parameters passed into the API for reporting mistakes.
    /// </summary>
    public class MistakeReportParameters
    {
        /// <summary>
        /// Your email to receive updates after Netcraft staff review this URL.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The reason why you believe this URL is incorrectly flagged.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The URL in question.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
