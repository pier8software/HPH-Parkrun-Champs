using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps; 

public class GetParkRunOfTheMonthStep(HphSpreadSheetAdapter hphSpreadSheetsAdapter) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        ctx.Status("Getting ParkRun of the Month...");
        data.ParkRunOfTheMonth = await hphSpreadSheetsAdapter.GetParkRunOfTheMonth(data.LastParkRunDate.Month);
        console.MarkupLine("[green]This month ParkRun is [bold]{0}[/][/]", data.ParkRunOfTheMonth);
    }
}