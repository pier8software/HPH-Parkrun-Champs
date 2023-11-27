using HPH.ParkRunChamps.Cli.Pipeline.Steps;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

public class ParkRunChampsPipeline {
    private readonly IList<IPipelineStep> _pipelineSteps = new List<IPipelineStep>();

    public async Task Execute(ParkRunChampsData data, IAnsiConsole console)
    {
        var font = FigletFont.Load("Graffiti.flf");
        console.Write(
            new FigletText(font, "HPH ParkRun Champs")
                .LeftJustified()
                .Color(Color.Red));

        await console.Status()
            .Spinner(Spinner.Known.Dots)
            .StartAsync("Updating ParkRun Champs...", async ctx =>
            {
                foreach (var step in _pipelineSteps)
                {
                    await step.ExecuteStep(data, console, ctx);
                }
            });
    }

    public void AddStep(IPipelineStep step)
    {
        _pipelineSteps.Add(step);
    }
}