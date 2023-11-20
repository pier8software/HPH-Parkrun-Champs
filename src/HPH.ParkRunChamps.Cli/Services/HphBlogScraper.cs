using System.Globalization;
using AngleSharp;
using AngleSharp.Dom;
using HPH.ParkRunChamps.Cli.Pipeline;

namespace HPH.ParkRunChamps.Cli;

public interface IHphBlogScraper {
    Task<(DateOnly date, Uri link)> GetLatestParkRunInfo();
    Task<Dictionary<string, IEnumerable<ParkRunResult>>> GetParkRunResults(Uri resultsLink);
}

public class HphBlogScraper : IHphBlogScraper {
    private readonly IBrowsingContext context;
    public HphBlogScraper()
    {
        var config = Configuration.Default.WithDefaultLoader();
        context = BrowsingContext.New(config);
    }

    public async Task<(DateOnly date, Uri link)> GetLatestParkRunInfo()
    {
        var address = "https://www.hydeparkharriers.co.uk/category/parkrun/";
        var document = await context.OpenAsync(address);
        var selector = "article h2 a";
        var article = document.QuerySelector(selector);
        var link = article.Attributes["href"].Value;
        var date = article.TextContent.Replace("parkrunday", string.Empty).Trim();
        var parsedDate = DateOnly.ParseExact(date, "d'th' MMMM yyyy", CultureInfo.InvariantCulture);
        return (parsedDate,new Uri(link));
    }

    public async Task<Dictionary<string, IEnumerable<ParkRunResult>>> GetParkRunResults(Uri resultsLink)
    {
        var resultsPage = await context.OpenAsync(resultsLink.AbsoluteUri);
        var tables = resultsPage.QuerySelectorAll("figure table");

        return new Dictionary<string, IEnumerable<ParkRunResult>>
        {
            ["Women"] = ParseResults(tables[0]),
            ["Men"] = ParseResults(tables[1]),
        };
    }

    private static IEnumerable<ParkRunResult> ParseResults(IElement table)
    {
        var rows = table.QuerySelectorAll("tbody tr:not(:first-child)");
        foreach (var row in rows)
        {
            var cells = row.QuerySelectorAll("td");
            yield return new ParkRunResult(cells[0].TextContent, cells[1].TextContent, cells[4].TextContent);
        }
    }
}