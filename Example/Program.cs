using System;
using System.Threading.Tasks;
using Netcraft;
using Netcraft.Entities;
using System.Linq;

namespace Example
{
    public static class Program
    {
        private static readonly NetcraftClient Client = new();

        public static async Task Main()
        {
            Console.WriteLine("Enter the email to submit a report from:");
            string email = Console.ReadLine();

            Console.WriteLine($"> Reporting a malicious URL");
            string uuid = await Client.Report.Urls(email, "Phishing against the Turkish Government.",
            [
                new()
                {
                    Value = "http://basvuruedevletmobilden.ml/",
                    Country = "US"
                }
            ]);

            Console.WriteLine($"Successfully reported, submission UUID: {uuid}");


            Console.WriteLine("\n> Getting submission's details");
            Submission submission = await Client.Submission.GetSubmissionDetails(uuid);

            Console.WriteLine($"Fetched submission's details, state: {submission.State}");


            Console.WriteLine("\n> Getting submission's URLs");
            SubmissionUrl[] submissionUrls = await Client.Submission.GetSubmissionUrls(uuid);

            Console.WriteLine($"Fetched {submissionUrls.Length} submission's URLs");


            Console.WriteLine("\n> Getting submission's email");
            SubmissionEmail submissionEmail = await Client.Submission.GetSubmissionEmail(uuid);

            Console.WriteLine(
                $"Fetched a submission email" +
                $"{(submissionEmail.State == State.Processing ? ", which is currently being processed" : $"with subject '{submissionEmail.Subject}'")}");


            Console.WriteLine("\n> Getting submission's cryptocurrency addresses");
            CryptocurrencyAddress[] submissionCryptoAddresses = await Client.Submission.GetSubmissionCryptocurrencyAddresses(uuid);

            Console.WriteLine($"Fetched {submissionCryptoAddresses.Length} cryptocurrency addresses");


            Console.WriteLine("\n> Getting submission's files");
            SubmissionFile[] submissionFiles = await Client.Submission.GetSubmissionFiles(uuid);

            Console.WriteLine($"Fetched {submissionFiles.Length} files");


            Console.WriteLine("\n> Getting the submission leaderboard");
            LeaderboardEntry[] entries = await Client.Misc.GetLeaderboard();

            Console.WriteLine($"Fetched the leaderboard, top 5 users: {string.Join(", ", entries.Take(5).Select(x => x.Nickname))}");


            Console.WriteLine("\nDemo finished");
            Console.ReadKey();
        }
    }
}