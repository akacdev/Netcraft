using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netcraft
{
    internal class Constants
    {
        public const string UserAgent = "Netcraft C# Client - actually-akac/Netcraft";

        public static readonly JsonSerializerOptions EnumOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(new EnumNamingPolicy())
            }
        };

        public const string SuccessReportMessage = "Successfully reported";
        public const string SuccessMistakeReportMessage = "Successfully reported mistake";
    }
}
