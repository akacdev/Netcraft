using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Netcraft.Entities
{
    /// <summary>
    /// A state of a submission, file, email or URL.
    /// </summary>
    public enum State
    {
        Processing,
        NoThreats,
        Unavailable,
        Suspicious,
        Malicious,
        Rejected,
        NotInjected,
        NotStarted,
        InProgress,
        Resolved
    }

    /// <summary>
    /// The direction to sort result in.
    /// </summary>
    public enum Direction
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// The property to sort files by.
    /// </summary>
    public enum FileSort
    {
        FileName,
        FileState
    }

    /// <summary>
    /// A submission on Netcraft of a URL, file or email.
    /// </summary>
    public class Submission
    {
        /// <summary>
        /// Whether the submission is still pending.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("pending")]
        public bool Pending { get; set; }

        /// <summary>
        /// The state of this submission.
        /// </summary>
        [JsonPropertyName("state")]
        public State State { get; set; }

        /// <summary>
        /// If this is an email, this represents the state of this email submission.
        /// </summary>
        [JsonPropertyName("mail_state")]
        public State MailState { get; set; }

        /// <summary>
        /// The reason for creating this submission, provided by the reporter.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// When this submission was created at.
        /// </summary>
        [JsonConverter(typeof(LongUnixConverter))]
        [JsonPropertyName("date")]
        public DateTime SubmittedAt { get; set; }

        /// <summary>
        /// When this submission was last updated at.
        /// </summary>
        [JsonConverter(typeof(LongUnixConverter))]
        [JsonPropertyName("last_update")]
        public DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Whether this submission has any cryptocurrency addresses embedded in it.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_cryptocurrency_addresses")]
        public bool HasCryptocurrencyAddresses { get; set; }

        /// <summary>
        /// Whether this submission has any files embedded in it.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_files")]
        public bool HasFiles { get; set; }

        /// <summary>
        /// Whether this submission has any issues.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_issues")]
        public bool HasIssues { get; set; }

        /// <summary>
        /// Whether this submission has any email.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_mail")]
        public bool HasEmail { get; set; }

        /// <summary>
        /// Whether this submission has phone numbers embedded in it.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_phone_numbers")]
        public bool HasPhoneNumbers { get; set; }

        /// <summary>
        /// Whether this submission has any URLs.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_urls")]
        public bool HasUrls { get; set; }

        /// <summary>
        /// Whether this submission is archived. Archived submissions are limited in the amount of data that can be retrieved through the API.
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("is_archived")]
        public bool IsArchived { get; set; }

        /// <summary>
        /// The source for this submission.
        /// </summary>
        [JsonPropertyName("source")]
        public ReportSource Source { get; set; }

        /// <summary>
        /// The original source for this submission.
        /// </summary>
        [JsonPropertyName("original_source")]
        public OriginalSource OriginalSource { get; set; }

        /// <summary>
        /// The classification log for this submission. Whenever a classification changes, it's logged here.
        /// </summary>
        [JsonPropertyName("classification_log")]
        public ClassificationLog[] ClassificationLog { get; set; }

        /// <summary>
        /// Counts of states for URLs and files present in this submission.
        /// </summary>
        [JsonPropertyName("state_counts")]
        public StateCounts StateCounts { get; set; }

        /// <summary>
        /// Information about this submission's creator.
        /// </summary>
        [JsonPropertyName("submitter")]
        public Submitter Submitter { get; set; }

        /// <summary>
        /// Tags that were attached to the submíssion.
        /// </summary>
        [JsonPropertyName("tags")]
        public SubmissionTag[] Tags { get; set; }
    }

    /// <summary>
    /// Keeps track of classification changes over time.
    /// </summary>
    public class ClassificationLog
    {
        /// <summary>
        /// When this change has been recorded.
        /// </summary>
        [JsonConverter(typeof(LongUnixConverter))]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The original state.
        /// </summary>
        [JsonPropertyName("from_state")]
        public State FromState { get; set; }

        /// <summary>
        /// The new state.
        /// </summary>
        [JsonPropertyName("to_state")]
        public State ToState { get; set; }
    }

    public class OriginalSource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class ReportSource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class StateCounts
    {
        [JsonPropertyName("files")]
        public Dictionary<State, int> Files { get; set; }

        [JsonPropertyName("urls")]
        public Dictionary<State, int> Urls { get; set; }
    }

    public class Submitter
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class SubmissionTag
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class SubmissionCryptocurrencyAddressContainer
    {
        [JsonPropertyName("cryptocurrency_addresses")]
        public CryptocurrencyAddress[] CryptocurrencyAddresses { get; set; }

        [JsonPropertyName("filtered_count")]
        public int FilteredCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class CryptocurrencyAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("cryptocurrency")]
        public string Cryptocurrency { get; set; }

        [JsonPropertyName("sources")]
        public FileSource[] Sources { get; set; }
    }

    public class FileSource
    {
        [JsonPropertyName("file_hash")]
        public string Hash { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }

    public class IssuesContainer
    {
        [JsonPropertyName("issues")]
        public Issue[] Issues { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class Issue
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("files")]
        public string[] FileHashes { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("locked")]
        public bool Locked { get; set; }

        [JsonPropertyName("messages")]
        public Message[] Messages { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("urls")]
        public IssueUrl[] Urls { get; set; }

        [JsonPropertyName("resolution")]
        public Resolution Resolution { get; set; }
    }

    public class IssueUrl
    {
        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_screenshot")]
        public bool HasScreenshot { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("url")]
        public string Value { get; set; }
    }
    
    public class Message
    {
        [JsonConverter(typeof(LongUnixConverter))]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("is_netcraft")]
        public bool IsNetcraft { get; set; }

        [JsonPropertyName("message")]
        public string Value { get; set; }
    }

    public class Resolution
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("response")]
        public string Response { get; set; }
    }

    public class MessageParameters
    {
        public string Message { get; set; }
    }

    public class SubmissionIssueParameters
    {
        [JsonPropertyName("additional_info")]
        public string AdditionalInfo { get; set; }

        [JsonPropertyName("filename_misclassifications")]
        public string[] FilenameMisclassifications { get; set; }

        [JsonPropertyName("url_misclassifications")]
        public UrlMisclassificationParameters[] UrlMisclassifications { get; set; }
    }

    public class UrlMisclassificationParameters
    {
        [JsonPropertyName("url")]
        public string Value { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("screenshot")]
        public ScreenshotParameters Screenshot { get; set; }
    }

    public class ScreenshotParameters
    {
        [JsonPropertyName("base64")]
        public string Base64 { get; set; }

        [JsonPropertyName("ext")]
        public string Extension { get; set; }
    }

    public class SubmissionUrlsContainer
    {
        [JsonPropertyName("filtered_count")]
        public int FilteredCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("urls")]
        public SubmissionUrl[] Urls { get; set; }
    }

    public class SubmissionUrl
    {
        [JsonPropertyName("uuid")]
        public string UUID { get; set; }

        [JsonPropertyName("url")]
        public string Value { get; set; }

        [JsonPropertyName("url_state")]
        public State State { get; set; }

        [JsonPropertyName("url_takedown_state")]
        public string UrlTakedownState { get; set; }

        [JsonPropertyName("classification_log")]
        public ClassificationLog[] ClassificationLog { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("file_hash")]
        public string FileHash { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("incident_report_url")]
        public string IncidentReportUrl { get; set; }

        [JsonPropertyName("ip")]
        public string IP { get; set; }

        [JsonPropertyName("screenshots")]
        public UrlScreenshot[] Screenshots { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("sources")]
        public UrlSource[] Sources { get; set; }

        [JsonPropertyName("tags")]
        public UrlTag[] Tags { get; set; }

        [JsonPropertyName("url_classification_reason")]
        public string ClassificationReason { get; set; }
    }

    public class UrlScreenshot
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class UrlSource
    {
        [JsonPropertyName("source")]
        public string Value { get; set; }

        [JsonPropertyName("source_id")]
        public int Id { get; set; }

        [JsonPropertyName("file_hash")]
        public string FileHash { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }
    }

    public class UrlTag
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("submitter_tag")]
        public int SubmitterTag { get; set; }
    }

    public class SubmissionEmail
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("state")]
        public State State { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("from")]
        public string[] From { get; set; }

        [JsonPropertyName("to")]
        public string[] To { get; set; }

        [JsonPropertyName("reply_to")]
        public string[] ReplyTo { get; set; }

        [JsonPropertyName("classification_log")]
        public ClassificationLog[] ClassificationLog { get; set; }
    }

    public class SubmissionFilesContainer
    {
        [JsonPropertyName("files")]
        public SubmissionFile[] Files { get; set; }

        [JsonPropertyName("filtered_count")]
        public int FilteredCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class SubmissionFile
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("file_state")]
        public State State { get; set; }

        [JsonPropertyName("filename")]
        public string Name { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonPropertyName("has_screenshot")]
        public bool HasScreenshot { get; set; }

        [JsonPropertyName("classification_log")]
        public ClassificationLog[] ClassificationLog { get; set; }
    }

    public class SubmissionPhoneNumberContainer
    {
        [JsonPropertyName("phone_numbers")]
        public SubmissionPhoneNumber[] PhoneNumbers { get; set; }

        [JsonPropertyName("filtered_count")]
        public int FilteredCount { get; set; }

        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }

    public class SubmissionPhoneNumber
    {
        [JsonPropertyName("phone_number")]
        public string Value { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }

    public class FileAnalysisUrlsContainer
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }

        [JsonPropertyName("urls")]
        public FileAnalysisUrl[] Urls { get; set; }
    }

    public class FileAnalysisUrl
    {
        [JsonPropertyName("url")]
        public string Value { get; set; }

        [JsonPropertyName("incident_report_url")]
        public string IncidentReportUrl { get; set; }

        [JsonPropertyName("state")]
        public State State { get; set; }

        [JsonPropertyName("url_takedown_state")]
        public State UrlTakedownState { get; set; }
    }

    public class FileMalwareFamily
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class FilePhoneNumber
    {
        [JsonPropertyName("phone_numbers")]
        public string[] PhoneNumbers { get; set; }
    }

    public class FileUrlsContainer
    {
        [JsonPropertyName("urls")]
        public FileUrlsData[] Data { get; set; }
    }

    public class FileUrlsData
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("urls")]
        public FileUrl[] Urls { get; set; }
    }

    public class FileUrl
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("url_state")]
        public string UrlState { get; set; }

        [JsonConverter(typeof(LongUnixConverter))]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("submission_uuid")]
        public string SubmissionUuid { get; set; }
    }
}