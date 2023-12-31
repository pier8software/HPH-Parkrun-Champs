using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

public class GetMembersStep(IHphSpreadSheetAdapter hphSpreadSheetsAdapter) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data,
        IAnsiConsole console,
        StatusContext ctx)
    {
        ctx.Status("Getting Club Members...");
        data.Members = await hphSpreadSheetsAdapter.GetMembersList();
        console.MarkupLine($"[green][bold]{data.Members.Count()}[/] Members Loaded[/]");
    }
}