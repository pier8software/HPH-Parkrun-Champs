using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

public class GetClubChampsResultsStep(HphSpreadSheetAdapter hphSpreadSheetAdapter) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        ctx.Status($"Fetching Club Champs results...");
        data.ClubChampResults = new Dictionary<string, IEnumerable<ClubChampResult>>
        {
            ["Women"] = await hphSpreadSheetAdapter.GetClubChampsForTheMonthAndGender(data.LastParkRunDate.Month, "Women"),
            ["Men"] = await hphSpreadSheetAdapter.GetClubChampsForTheMonthAndGender(data.LastParkRunDate.Month, "Men")
        };
        console.MarkupLine($"[green]Received [bold]{data.ClubChampResults.Values.SelectMany(x => x).Count()}[/] Club Champs results[/]");
    }
}