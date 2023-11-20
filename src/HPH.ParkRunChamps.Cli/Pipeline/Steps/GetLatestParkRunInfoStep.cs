using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

public class GetLatestParkRunInfoStep(IHphBlogScraper scraper) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        ctx.Status("Getting Latest ParkRun Info...");
        var (date, link) = await scraper.GetLatestParkRunInfo();

        data.LastParkRunDate = date;
        data.ResultsLink = link;
        
        console.MarkupLine($"[green]Latest Published ParkRun Results Date: [bold]{date:dd/MM/yyyy}[/][/]");
    }
}