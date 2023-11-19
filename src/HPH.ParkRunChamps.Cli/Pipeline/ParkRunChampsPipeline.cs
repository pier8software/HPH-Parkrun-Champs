using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

public class ParkRunChampsPipeline {
    private readonly IList<IPipelineStep> _pipelineSteps = new List<IPipelineStep>();

    public async Task Execute(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        foreach (var step in _pipelineSteps)
        {
            await step.ExecuteStep(data, console, ctx);
        }
    }

    public void AddStep(IPipelineStep step)
    {
        _pipelineSteps.Add(step);
    }
}