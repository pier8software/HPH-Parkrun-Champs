using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

public interface IPipelineStep {
    Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx);
}