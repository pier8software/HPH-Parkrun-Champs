using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

internal class GetMembersStep(GoogleSheetAdapter googleSheetsAdapter) : IPipelineStep {
    public async Task ExecuteStep(ParkRunChampsData data,
        IAnsiConsole console,
        StatusContext ctx)
    {
        ctx.Status("Getting Club Members...");
        data.Members = await googleSheetsAdapter.GetMembersList();
        console.MarkupLine($"[green][bold]{data.Members.Count()}[/] Members Loaded...[/]");
    }
}