using Netcraft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Netcraft
{
    public static class API
    {
        public const int MaxRetries = 3;
        public const int RetryDelay = 1000 * 1;
        public const int PreviewMaxLength = 500;

        public static async Task<HttpResponseMessage> Request
        (
            this HttpClient cl,
            HttpMethod method,
            string url,
            object obj,
            HttpStatusCode target = HttpStatusCode.OK,
            JsonSerializerOptions options = null)
        => await Request(cl, method, url, new StringContent(JsonSerializer.Serialize(obj, options ?? Constants.EnumOptions), Encoding.UTF8, "application/json"), target);

        public static async Task<HttpResponseMessage> Request
        (
            this HttpClient cl,
            HttpMethod method,
            string url,
            HttpContent content = null,
            HttpStatusCode target = HttpStatusCode.OK)
        {
            HttpRequestMessage req = new(method, url)
            {
                Content = content
            };

            HttpResponseMessage res = await cl.SendAsync(req);

            if ((int)res.StatusCode > 500) throw new NetcraftException("Received a failure status code.");

            if (!target.HasFlag(res.StatusCode))
            {
                string text = await res.Content.ReadAsStringAsync();

                MediaTypeHeaderValue contentType = res.Content.Headers.ContentType;
                if (contentType is null) throw new NetcraftException("The 'Content-Type' header is missing in the response.", method.ToString(), url);

                bool isJson = contentType.MediaType.StartsWith("application/json", StringComparison.InvariantCultureIgnoreCase);
                if (!isJson)
                    throw new NetcraftException(
                            $"received status code {res.StatusCode} and Content-Type {contentType.MediaType}" +
                            $"\nPreview: {text[..Math.Min(text.Length, PreviewMaxLength)]}",
                        method.ToString(),
                        url);

                NetcraftError error = await res.Deseralize<NetcraftError>();
                if (error is null) throw new NetcraftException("Parsed error object is null.", method.ToString(), url);

                StringBuilder sb = new();
                sb.AppendLine("Operation resulted in the following API error:");
                sb.AppendLine($"\nStatus: {error.Status}");
                if (!string.IsNullOrEmpty(error.Description)) sb.AppendLine($"\nDescription: {error.Description}");

                if (error.Details is not null && error.Details.Length > 0)
                {
                    for (int i = 0; i < error.Details.Length; i++)
                    {
                        ErrorDetail detail = error.Details[i];

                        sb.AppendLine($"[#{i + 1}] {detail.Message} caused by input '{detail.Input}' at '{detail.Path}'");
                    }
                }

                throw new NetcraftException(sb.ToString());
            }

            return res;
        }

        public static async Task<T> Deseralize<T>(this HttpResponseMessage res, JsonSerializerOptions options = null)
        {
            string text = await res.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(text)) throw new NetcraftException("Response content is empty, can't parse as JSON.");

            try
            {
                return JsonSerializer.Deserialize<T>(text, options ?? Constants.EnumOptions);
            }
            catch (Exception ex)
            {
                throw new NetcraftException($"Exception while parsing JSON: {ex.GetType().Name} => {ex.Message}\nPreview: {text[..Math.Min(text.Length, PreviewMaxLength)]}");
            }
        }
    }
}