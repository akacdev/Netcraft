# Netcraft

![](https://raw.githubusercontent.com/actually-akac/Netcraft/master/Netcraft/banner.png)

### An async and lightweight C# library for interacting with the Netcraft API.

## Usage
This library provides an easy interface for interacting with the Netcraft API. Most importantly, it empowers you to seamlessly automate the reporting of malicious URLs and emails to Netcraft.

To get started, import the library into your solution with either the `NuGet Package Manager` or the `dotnet` CLI.
```rust
dotnet add package Netcraft
```

For the primary classes to become available, import one or more of the used namespaces.
```csharp
using Netcraft;
using Netcraft.Modules;
using Netcraft.Entities;
```

Need more examples? Under the `Example` directory you can find a working demo project that implements this library.

## Properties
- Built for **.NET 8**, **.NET 7** and **.NET 6**
- Fully **async**
- Extensive **XML documentation**
- **No external dependencies** (makes use of built-in `HttpClient` and `JsonSerializer`)
- **Custom exceptions** (`NetcraftException`) for easy debugging
- Parsing of custom Netcraft errors
- Example project to demonstrate all capabilities of the library

## Features
- Report malicious URLs, emails and misclassifications
- Request details about existing submissions
- Fetch the reporter leaderboard

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

## References
- https://www.netcraft.com/
- https://report.netcraft.com
- https://report.netcraft.com/api/v3

*This is a community-ran library. Not affiliated with Netcraft Ltd.*