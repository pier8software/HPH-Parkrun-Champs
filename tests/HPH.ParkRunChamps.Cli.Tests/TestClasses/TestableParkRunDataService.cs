using HPH.ParkRunChamps.Cli.Pipeline;

namespace HPH.ParkRunChamps.Cli.Tests.TestClasses; 

public class TestableHphBlogScraper : IHphBlogScraper {
    public Task<Dictionary<string, IEnumerable<ParkRunResult>>> GetParkRunResults(Uri resultsLink)
    {
        var results = new Dictionary<string, IEnumerable<ParkRunResult>>
        {
            ["Women"] = new List<ParkRunResult>(),
            ["Men"] = new List<ParkRunResult>()
        };
        return Task.FromResult(results);
    }

    public Task<(DateOnly date, Uri link)> GetLatestParkRunInfo()
    {
        var dateTime = new DateOnly(1985, 10, 26);
        var linkToResults = new Uri("https://example.com");
        return Task.FromResult((dateTime, linkToResults));
    }
}