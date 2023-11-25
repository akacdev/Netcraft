using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Netcraft.Entities;
using System.Net;
using System.IO;
using System;

namespace Netcraft.Modules
{
    public class SubmissionModule
    {
        private readonly HttpClient Client;

        public SubmissionModule(HttpClient client)
        {
            Client = client;
        }

        //Submission

        /// <summary>
        /// Get details about a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<Submission> GetSubmissionDetails(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get, $"submission/{uuid.UrlEncode()}");

            return await res.Deseralize<Submission>();
        }

        /// <summary>
        /// Get issues on a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<Issue[]> GetSubmissionIssues(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get, $"submission/{uuid.UrlEncode()}/issues");

            return (await res.Deseralize<IssuesContainer>()).Issues;
        }

        /// <summary>
        /// Get URLs reported in a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="count">The maximum amount of results to return.</param>
        /// <param name="urlFilter"> The URL to filter by.</param>
        /// <param name="urlStateFilter">The URL state to filter by.</param>
        /// <param name="urlTakedownStateFilter">The URL takedown state to filter by.</param>
        /// <param name="direction">The direction in which to sort.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<SubmissionUrl[]> GetSubmissionUrls(
            string uuid, int count = 100, string urlFilter = null, State? urlStateFilter = null, State? urlTakedownStateFilter = null, Direction direction = Direction.Ascending)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count is zero or a negative number.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                    $"submission/{uuid.UrlEncode()}/urls" +
                    $"?count={count}" +
                    $"&dir={direction.ToApi()}" +
                    (urlFilter is null ? "" : $"&url={urlFilter}") +
                    (urlStateFilter is null ? "" : $"&url_state={urlStateFilter.Value.ToApi()}") +
                    (urlTakedownStateFilter is null ? "" : $"&url_takedown_state={urlTakedownStateFilter.Value.ToApi()}"));

            SubmissionUrlsContainer container = await res.Deseralize<SubmissionUrlsContainer>();

            return container.Urls;
        }

        /// <summary>
        /// Get an email embedded within a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<SubmissionEmail> GetSubmissionEmail(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get, $"submission/{uuid.UrlEncode()}/mail");

            return await res.Deseralize<SubmissionEmail>();
        }

        /// <summary>
        /// Get cryptocurrency addresses embedded within a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="count">The maximum amount of results to return.</param>
        /// <param name="addressFilter">The cryptocurrency address to filter by.</param>
        /// <param name="cryptocurrencyFilter">The cryptocurrency name to filter by.</param>
        /// <param name="sortProperty">The property to sort by.</param>
        /// <param name="direction">The direction in which to sort.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<CryptocurrencyAddress[]> GetSubmissionCryptocurrencyAddresses(
            string uuid, int count = 100, string addressFilter = null, string cryptocurrencyFilter = null, string sortProperty = null, Direction direction = Direction.Ascending)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count is zero or a negative number.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/cryptocurrency_addresses" +
                    $"?count={count}" +
                    $"&dir={direction.ToApi()}" +
                    (addressFilter is null ? "" : $"&address={addressFilter}") +
                    (cryptocurrencyFilter is null ? "" : $"&cryptocurrency={cryptocurrencyFilter}") +
                    (sortProperty is null ? "" : $"&sort={sortProperty}"));

            SubmissionCryptocurrencyAddressContainer container = await res.Deseralize<SubmissionCryptocurrencyAddressContainer>();

            return container.CryptocurrencyAddresses;
        }

        /// <summary>
        /// Get files embedded within a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="count">The maximum amount of results to return.</param>
        /// <param name="stateFilter">The state to filter by.</param>
        /// <param name="hashFilter">The hash to filter by.</param>
        /// <param name="nameFilter">The file name to filter by.</param>
        /// <param name="sort">The direction in which to sort.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<SubmissionFile[]> GetSubmissionFiles(
            string uuid, int count = 100, State? stateFilter = null, string hashFilter = null, string nameFilter = null, FileSort sort = FileSort.FileName)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count is zero or a negative number.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files" +
                    $"?count={count}" +
                    $"&sort={sort.ToApi()}" +
                    (stateFilter is null ? "" : $"&file_state={stateFilter.Value.ToApi()}") +
                    (hashFilter is null ? "" : $"&hash={hashFilter}") +
                    (nameFilter is null ? "" : $"&filename={nameFilter}"));

            SubmissionFilesContainer container = await res.Deseralize<SubmissionFilesContainer>();

            return container.Files;
        }

        /// <summary>
        /// Get phone numbers embedded within a submission.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<SubmissionPhoneNumber[]> GetSubmissionPhoneNumbers(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get, $"submission/{uuid.UrlEncode()}/phone_numbers");

            return (await res.Deseralize<SubmissionPhoneNumberContainer>()).PhoneNumbers;
        }

        /// <summary>
        /// Send a message about an issue. These messages will then be responded to by Netcraft staff. To avoid spam, only one message may be sent at a time.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="message">The message text to send.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task SendIssueMessage(string uuid, string message)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message), "Message is null.");

            await Client.Request(HttpMethod.Post, $"submission/{WebUtility.UrlEncode(uuid)}/issues/send_message", new MessageParameters() { Message = message });
        }

        /// <summary>
        /// Report an issue with a submission. You can use this to <b>report false negatives</b> and additional info can be attached.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="parameters">The parameters for submitting this issue.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task ReportSubmissionIssue(string uuid, SubmissionIssueParameters parameters)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (parameters is null) throw new ArgumentNullException(nameof(parameters), "Parameters are null.");
            if (parameters.AdditionalInfo is null) throw new ArgumentNullException(nameof(parameters), "Additional info is null.");
            if (parameters.AdditionalInfo.Length > 10000) throw new ArgumentOutOfRangeException(nameof(parameters), "Additional info is over 10 000 characters.");
            if (parameters.UrlMisclassifications is null) throw new ArgumentNullException(nameof(parameters), "URL misclassifications are null.");
            if (parameters.UrlMisclassifications.Length == 0) throw new ArgumentException("No URL misclassifications were provided.", nameof(parameters));
            if (parameters.FilenameMisclassifications is null) throw new ArgumentNullException(nameof(parameters), "File misclassifications are null.");
            if (parameters.FilenameMisclassifications.Length == 0) throw new ArgumentException("No file misclassifications were provided.", nameof(parameters));

            await Client.Request(HttpMethod.Post, $"submission/{WebUtility.UrlEncode(uuid)}/report_issue", parameters);
        }

        //URL

        /// <summary>
        /// Get a screenshot of a URL.
        /// </summary>
        /// <param name="submissionUuid">The <c>UUID</c> of this submission.</param>
        /// <param name="urlUuid">The <c>UUID</c> of this URL.</param>
        /// <param name="screenshotHash">The hash of this screenshot.</param>
        /// <returns>A stream of the image. Either <c>image/png</c> or <c>image/gif</c>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<Stream> GetUrlScreenshot(string submissionUuid, string urlUuid, string screenshotHash)
        {
            if (string.IsNullOrEmpty(submissionUuid)) throw new ArgumentNullException(nameof(submissionUuid), "Submission UUID is null.");
            if (string.IsNullOrEmpty(urlUuid)) throw new ArgumentNullException(nameof(urlUuid), "URL UUID is null.");
            if (string.IsNullOrEmpty(screenshotHash)) throw new ArgumentNullException(nameof(screenshotHash), "Screenshot hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{submissionUuid.UrlEncode()}/urls/{urlUuid.UrlEncode()}/screenshots/{screenshotHash.UrlEncode()}");

            return await res.Content.ReadAsStreamAsync();
        }

        //File

        /// <summary>
        /// Get the analysis of a file.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="fileHash">The hash of this file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<FileAnalysisUrl[]> GetFileAnalysisUrls(string uuid, string fileHash)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(fileHash)) throw new ArgumentNullException(nameof(fileHash), "File hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files/{fileHash.UrlEncode()}/analysis_urls");

            return (await res.Deseralize<FileAnalysisUrlsContainer>()).Urls;
        }

        /// <summary>
        /// Get the malware families of a file.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="fileHash">The hash of this file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<FileMalwareFamily[]> GetFileMalwareFamilies(string uuid, string fileHash)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(fileHash)) throw new ArgumentNullException(nameof(fileHash), "File hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files/{fileHash.UrlEncode()}/malware_families");

            return await res.Deseralize<FileMalwareFamily[]>();
        }

        /// <summary>
        /// Get the phone numbers embedded within a file.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="fileHash">The hash of this file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<FilePhoneNumber[]> GetFilePhoneNumbers(string uuid, string fileHash)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(fileHash)) throw new ArgumentNullException(nameof(fileHash), "File hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files/{fileHash.UrlEncode()}/phonenumbers");

            return await res.Deseralize<FilePhoneNumber[]>();
        }

        /// <summary>
        /// Get the screenshot of a file.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="fileHash">The hash of this file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<Stream> GetFileScreenshot(string uuid, string fileHash)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(fileHash)) throw new ArgumentNullException(nameof(fileHash), "File hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files/{fileHash.UrlEncode()}/screenshot");

            return await res.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Get the URLs embedded in a file.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <param name="fileHash">The hash of this file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<FileUrl[]> GetFileUrls(string uuid, string fileHash)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");
            if (string.IsNullOrEmpty(fileHash)) throw new ArgumentNullException(nameof(fileHash), "File hash is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/files/{fileHash.UrlEncode()}/urls");

            return (await res.Deseralize<FileUrlsContainer[]>()).SelectMany(x => x.Data).SelectMany(x => x.Urls).ToArray();
        }

        //Email

        /// <summary>
        /// Get the screenshot of an email.
        /// </summary>
        /// <param name="uuid">The <c>UUID</c> of this submission.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NetcraftException"></exception>
        public async Task<Stream> GetEmailScreenshot(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) throw new ArgumentNullException(nameof(uuid), "UUID is null.");

            HttpResponseMessage res = await Client.Request(HttpMethod.Get,
                $"submission/{uuid.UrlEncode()}/mail/screenshot");

            return await res.Content.ReadAsStreamAsync();
        }
    }
}