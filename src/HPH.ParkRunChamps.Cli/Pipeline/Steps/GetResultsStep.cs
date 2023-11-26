using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

public class GetResultsStep(IHphBlogScraper scraper) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        ctx.Status("Fetching ParkRun Results...");
        var results = await scraper.GetParkRunResults(data.ResultsLink);
        data.ParkRunResults = results;
        console.MarkupLineInterpolated($"[green]Received ParkRun Results for [bold]{results.Values.SelectMany(x => x).Count()}[/] Park Runners[/]");
    }
}