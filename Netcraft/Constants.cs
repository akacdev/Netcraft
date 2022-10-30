using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netcraft
{
    internal class Constants
    {
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
