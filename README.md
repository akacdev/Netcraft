# Urlscan

<div align="center">
  <img width="330" height="95" src="https://www.netcraft.com/assets/images/logo@2x.png">
</div>

<div align="center">
  An async and lightweight C# library for interacting with the Netcraft API.
</div>

## Usage
Available on NuGet as `Netcraft`, methods can be found under the classes `NetcraftClient`.<br>
https://www.nuget.org/packages/Netcraft

## Features
- Made with **.NET 6**
- Fully **async**
- Extensive **documentation**
- **No external dependencies** (uses integrated HTTP and JSON)
- **Custom exceptions** (`NetcraftException`) for advanced catching
- Example project to demonstrate all capabilities of the library
- Report malicious URLs, emails and files
- Get submission results and data

## Example
Under the `Example` directory you can find a working demo project that implements this library.

## Code Samples

### Initializing a new API client
```csharp
NetcraftClient client = new();
```

### Reporting malicious URLs
```csharp
string uuid = await client.Report.Urls(email, "Phishing against the Turkish Government.", new UrlReportParmeters[]
{
    new()
    {
        Value = "http://basvuruedevletmobilden.ml/",
        Country = "US"
    }
});
```

### Getting submission details
```csharp
Submission submission = await client.Submission.GetSubmissionDetails(uuid);
```

### Getting the leaderboard
```csharp
LeaderboardEntry[] entries = await client.Misc.GetLeaderboard();
```

## Available Methods

### **Report**

- Task\<string> **Urls**(string email, string reason, UrlReportParmeters[] urls, string source = null, bool? noSubmission = false)
- Task\<string> **Urls**(UrlsReportParameters parameters)
- Task\<string> **Email**(EmailReportParameters parameters)
- Task **Mistake**(MistakeReportParameters parameters)

### **Submission**

- Task **ReportSubmissionIssue**(string uuid, SubmissionIssueParameters parameters)
- Task **SendIssueMessage**(string uuid, string message)
- Task\<CryptocurrencyAddress[]> **GetSubmissionCryptocurrencyAddresses**( string uuid, int count = 100, string addressFilter = null, string cryptocurrencyFilter = null, string sortProperty = null, Direction direction = Direction.Ascending)
- Task\<FileAnalysisUrl[]> **GetFileAnalysisUrls**(string uuid, string fileHash)
- Task\<FileMalwareFamily[]> **GetFileMalwareFamilies**(string uuid, string fileHash)
- Task\<FilePhoneNumber[]> **GetFilePhoneNumbers**(string uuid, string fileHash)
- Task\<FileUrl[]> **GetFileUrls**(string uuid, string fileHash)
- Task\<Issue[]> **GetSubmissionIssues**(string uuid)
- Task\<Stream> **GetEmailScreenshot**(string uuid)
- Task\<Stream> **GetFileScreenshot**(string uuid, string fileHash)
- Task\<Stream> **GetUrlScreenshot**(string submissionUUID, string urlUUID, string screenshotHash)
- Task\<Submission> **GetSubmissionDetails**(string uuid)
- Task\<SubmissionEmail> **GetSubmissionEmail**(string uuid)
- Task\<SubmissionFile[]> **GetSubmissionFiles**( string uuid, int count = 100, State? stateFilter = null, string hashFilter = null, string nameFilter = null, FileSort sort = FileSort.FileName)
- Task\<SubmissionPhoneNumber[]> **GetSubmissionPhoneNumbers**(string uuid)
- Task\<SubmissionUrl[]> **GetSubmissionUrls**( string uuid, int count = 100, string urlFilter = null, State? urlStateFilter = null, State? urlTakedownStateFilter = null, Direction direction = Direction.Ascending)

### **Misc**

- Task\<LeaderboardEntry[]> **GetLeaderboard**()

## Official Links
https://www.netcraft.com/<br>
https://en.wikipedia.org/wiki/Netcraft

*This is a community-ran library. Not affiliated with Netcraft Ltd.*