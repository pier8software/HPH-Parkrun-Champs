using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

internal class UpdateResultsStep : IPipelineStep {
    public Task ExecuteStep(ParkRunChampsData data,
        IAnsiConsole console,
        StatusContext ctx)
    {
        // 1pbyEGcux3ResvlU4bXG5zuF39sZ0mOhJmD2e48Jp3pc
        console.WriteLine("Not Implemented");
        return Task.CompletedTask;
    }
}