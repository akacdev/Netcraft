using System.Text.Json.Serialization;

namespace Netcraft.Entities
{
    /// <summary>
    /// Server-side generated error message.
    /// </summary>
    public class NetcraftError
    {
        /// <summary>
        /// Details about this error.
        /// </summary>
        [JsonPropertyName("details")]
        public ErrorDetail[] Details { get; set; }

        /// <summary>
        /// Short description of the error that occurred.
        /// </summary>
        [JsonPropertyName("error")]
        public string Description { get; set; }

        /// <summary>
        /// The status code for this error.
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }

    /// <summary>
    /// Details for an <see cref="NetcraftError"/>.
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// Name of the input that triggered this error.
        /// </summary>
        [JsonPropertyName("input")]
        public string Input { get; set; }

        /// <summary>
        /// A detailed message describing this error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Path in your input that triggered this error.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}
