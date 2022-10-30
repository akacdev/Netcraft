using System;
using System.Threading.Tasks;
using Netcraft;
using Netcraft.Modules;
using Netcraft.Entities;
using System.Linq;

namespace Example
{
    public static class Program
    {
        public static async Task Main()
        {
            NetcraftClient client = new();

            Console.WriteLine("Enter the email to submit a report from:");
            string email = Console.ReadLine();

            Console.WriteLine($"> Reporting a malicious URL");
            string uuid = await client.Report.Urls(email, "Phishing against the Turkish Government.", new UrlReportParmeters[]
            {
                new()
                {
                    Value = "http://basvuruedevletmobilden.ml/",
                    Country = "US"
                }
            });

            Console.WriteLine($"Successfully reported, submission UUID: {uuid}");

            Console.WriteLine("> Getting submission's details");

            Submission submission = await client.Submission.GetSubmissionDetails(uuid);

            Console.WriteLine($"Fetched submission's details, state: {submission.State}");

            Console.WriteLine("> Getting submission's URLs");

            SubmissionUrl[] submissionUrls = await client.Submission.GetSubmissionUrls(uuid);

            Console.WriteLine($"Fetched {submissionUrls.Length} submission's URLs");

            Console.WriteLine("> Getting submission's email");

            SubmissionEmail submissionEmail = await client.Submission.GetSubmissionEmail(uuid);

            Console.WriteLine(
                $"Fetched a submission email" +
                $"{(submissionEmail.State == State.Processing ? ", which is currently being processed" : $"with subject '{submissionEmail.Subject}'")}");

            Console.WriteLine("> Getting submission's cryptocurrency addresses");

            CryptocurrencyAddress[] submissionCryptoAddresses = await client.Submission.GetSubmissionCryptocurrencyAddresses(uuid);

            Console.WriteLine($"Fetched {submissionCryptoAddresses.Length} cryptocurrency addresses");

            Console.WriteLine("> Getting submission's files");

            SubmissionFile[] submissionFiles = await client.Submission.GetSubmissionFiles(uuid);

            Console.WriteLine($"Fetched {submissionFiles.Length} files");

            Console.WriteLine("> Getting the submission leaderboard");

            LeaderboardEntry[] entries = await client.Misc.GetLeaderboard();

            Console.WriteLine($"Fetched the leaderboard, top 5 users: {string.Join(", ", entries.Take(5).Select(x => x.Nickname))}");

            Console.WriteLine("Demo finished");
            Console.ReadKey();
        }
    }
}