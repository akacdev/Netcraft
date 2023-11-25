using Netcraft.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Netcraft
{
    internal static class API
    {
        public static async Task<HttpResponseMessage> Request
        (
            this HttpClient cl,
            HttpMethod method,
            string path,
            object obj,
            HttpStatusCode target = HttpStatusCode.OK,
            JsonSerializerOptions options = null)
        => await Request(cl, method, path, await obj.Serialize(options ?? Constants.EnumOptions), target);

        public static async Task<HttpResponseMessage> Request
        (
            this HttpClient cl,
            HttpMethod method,
            string path,
            HttpContent content = null,
            HttpStatusCode target = HttpStatusCode.OK)
        {
            using HttpRequestMessage req = new(method, path)
            {
                Content = content
            };

            HttpResponseMessage res = await cl.SendAsync(req);
            content?.Dispose();

            if (target.HasFlag(res.StatusCode)) return res;

            NetcraftError error = await res.Deseralize<NetcraftError>() ??
                throw new NetcraftException($"Failed to request {method} {path}, received status code {res.StatusCode}\nPreview: {await res.GetPreview()}", res);

            StringBuilder sb = new();

            sb.AppendLine($"Failed to request {method} {path}, received the following API error:");
            sb.AppendLine($"Status: {error.Status}");
            if (!string.IsNullOrEmpty(error.Description)) sb.AppendLine($"Description: {error.Description}");

            if (error.Details is not null && error.Details.Length > 0)
            {
                for (int i = 0; i < error.Details.Length; i++)
                {
                    ErrorDetail detail = error.Details[i];

                    sb.AppendLine(string.Concat(
                        $"[#{i + 1}] {detail.Message}",
                        (string.IsNullOrEmpty(detail.Input) || string.IsNullOrEmpty(detail.Path)) ? "" : $" caused by input \"{detail.Input}\" at \"{detail.Path}\""));
                }
            }

            throw new NetcraftException(sb.ToString(), res);
        }
    }
}