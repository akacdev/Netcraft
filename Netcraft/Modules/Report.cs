using System.Threading.Tasks;
using System.Net.Http;
using Netcraft.Entities;
using System;

namespace Netcraft.Modules
{
    public class ReportModule
    {
        private readonly HttpClient Client;

        public ReportModule(HttpClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Report malicious URLs to Netcraft for processing.
        /// <para>
        ///     Netcraft accepts the following malicious URL types:<br/>
        ///     <i>Phishing</i>, <i>Malware</i>, <i>Cryptocurrency Scam</i>, <i>Fake Tech Support Scam</i>, <i>Fake Shop</i>, <i>Web Shell</i>, <i>Phishing Kit</i>.<br/>
        ///     <b>More Details</b>: <a href="https://report.netcraft.com/faqs"></a>
        /// </para>
        /// </summary>
        /// <param name="email">Your email for receiving confirmation and classification updates.</param>
        /// <param name="reason">The reason for reporting.</param>
        /// <param name="urls">The URLs to report.</param>
        /// <param name="source">The source UUID. Only include if you were provided with one.</param>
        /// <param name="noSubmission">Enables reporting without creating submissions. This option is only available for a subset of reporters.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        /// <returns></returns>
        public async Task<string> Urls(string email, string reason, UrlReportParmeters[] urls, string source = null, bool? noSubmission = false)
            => await Urls(new UrlsReportParameters() { Email = email, Reason = reason, Urls = urls, Source = source, NoSubmission = noSubmission });

        /// <summary>
        /// Report malicious URLs to Netcraft for processing.
        /// <para>
        ///     Netcraft accepts the following malicious URL types:<br/>
        ///     <i>Phishing</i>, <i>Malware</i>, <i>Cryptocurrency Scam</i>, <i>Fake Tech Support Scam</i>, <i>Fake Shop</i>, <i>Web Shell</i>, <i>Phishing Kit</i>.<br/>
        ///     <b>More Details</b>: <a href="https://report.netcraft.com/faqs"></a>
        /// </para>
        /// </summary>
        /// <param name="parameters">The parameters for reporting.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<string> Urls(UrlsReportParameters parameters)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters), "URLs report parameters are null.");
            if (string.IsNullOrEmpty(parameters.Email)) throw new ArgumentNullException(nameof(parameters.Email), "Email is null.");
            if (string.IsNullOrEmpty(parameters.Reason)) throw new ArgumentNullException(nameof(parameters.Reason), "Reason is null.");
            if (parameters.Reason.Length > 10000) throw new ArgumentOutOfRangeException(nameof(parameters.Reason), "Reason is over 10 000 characters.");
            if (parameters.Urls is null) throw new ArgumentNullException(nameof(parameters.Urls), "URLs are null.");
            if (parameters.Urls.Length == 0) throw new ArgumentException("No URLs were provided.", nameof(parameters));

            HttpResponseMessage res = await Client.Request(HttpMethod.Post, "report/urls", parameters);

            ReportResult result = await res.Deseralize<ReportResult>();
            if (result.Message != Constants.SuccessReportMessage)
                throw new NetcraftException($"URLs reporting response has an unexpected message: {result.Message}");

            return result.UUID;
        }

        /// <summary>
        /// Report malicious emails to Netcraft for processing.
        /// </summary>
        /// <param name="email">Your email for receiving confirmation and classification updates.</param>
        /// <param name="message">
        ///     One of:<br/>
        ///     1) Plain text string of the malicious email in <b>MIME format</b><br/>
        ///     2) If the password parameter is provided: A base64 encoded <b>AES-256-CBC</b> encrypted email in <b>MIME format</b>. Max message size is 20MiB.
        /// </param>
        /// <param name="password">The password if the email is encrypted.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        /// <returns></returns>
        public async Task<string> Email(string email, string message, string password = null)
            => await Email(new EmailReportParameters() { Email = email, Message = message, Password = password });

        /// <summary>
        /// Report malicious emails to Netcraft for processing.
        /// </summary>
        /// <param name="parameters">The parameters for reporting.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<string> Email(EmailReportParameters parameters)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters), "Email report parameters are null.");
            if (string.IsNullOrEmpty(parameters.Email)) throw new ArgumentNullException(nameof(parameters.Email), "Email is null.");
            if (string.IsNullOrEmpty(parameters.Message)) throw new ArgumentNullException(nameof(parameters.Message), "Message is null.");
            if (parameters.Message.Length > 20971520) throw new ArgumentOutOfRangeException(nameof(parameters.Message), "Reason is over 20 MiB.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Post, "report/email", parameters);

            ReportResult result = await res.Deseralize<ReportResult>();
            if (result.Message != Constants.SuccessReportMessage)
                throw new NetcraftException($"Email reporting response has an unexpected message: {result.Message}");

            return result.UUID;
        }

        /// <summary>
        /// Report a mistake (false-positive) detection to Netcraft for correction.
        /// </summary>
        /// <param name="email">Your email to receive updates after Netcraft staff review this URL.</param>
        /// <param name="reason">The reason why you believe this URL is incorrectly flagged.</param>
        /// <param name="url">The URL in question.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task Mistake(string email, string reason, string url)
            => await Mistake(new MistakeReportParameters() { Email = email, Reason = reason, Url = url });

        /// <summary>
        /// Report a mistake (false-positive) detection to Netcraft for correction.
        /// </summary>
        /// <param name="parameters">The parameters for reporting.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task Mistake(MistakeReportParameters parameters)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters), "Email report parameters are null.");
            if (string.IsNullOrEmpty(parameters.Email)) throw new ArgumentNullException(nameof(parameters.Email), "Email is null.");
            if (string.IsNullOrEmpty(parameters.Reason)) throw new ArgumentNullException(nameof(parameters.Reason), "Reason is null.");
            if (string.IsNullOrEmpty(parameters.Url)) throw new ArgumentNullException(nameof(parameters.Url), "URL is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Post, "report/mistake", parameters);

            ReportResult result = await res.Deseralize<ReportResult>();
            if (result.Message != Constants.SuccessMistakeReportMessage)
                throw new NetcraftException($"Mistake reporting response has an unexpected message: {result.Message}");
        }
    }
}