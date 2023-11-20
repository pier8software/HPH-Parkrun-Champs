using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

public class GetResultsStep(IHphBlogScraper scraper) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        ctx.Status("Fetching Results...");
        var results = await scraper.GetParkRunResults(data.ResultsLink);
        data.Results = results;
        console.MarkupLineInterpolated($"[green]Received Results for [bold]{results.Values.SelectMany(x => x).Count()}[/] Park Runners[/]");
    }
}