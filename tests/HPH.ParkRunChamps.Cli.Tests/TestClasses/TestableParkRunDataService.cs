using HPH.ParkRunChamps.Cli.Pipeline;

namespace HPH.ParkRunChamps.Cli.Tests.TestClasses; 

public class TestableHphBlogScraper : IHphBlogScraper {
    public Task<IDictionary<string, IEnumerable<ParkRunResult>>> GetParkRunResults(Uri resultsLink)
    {
        throw new NotImplementedException();
    }

    public Task<(DateOnly date, Uri link)> GetLatestParkRunInfo()
    {
        var dateTime = new DateOnly(1985, 10, 26);
        var linkToResults = new Uri("https://example.com");
        return Task.FromResult((dateTime, linkToResults));
    }
}