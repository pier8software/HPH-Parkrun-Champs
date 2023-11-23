using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline;

public class ParkRunChampsPipeline {
    private readonly IList<IPipelineStep> _pipelineSteps = new List<IPipelineStep>();

    public async Task Execute(ParkRunChampsData data, IAnsiConsole console)
    {
        console.Markup(@"[red]
ooooo ooooo oooooooooo  ooooo ooooo    oooooooooo                          oooo        oooooooooo                               [white]oooooooo8 oooo[/]                                                          
 888   888   888    888  888   888      888    888   ooooooo   oo oooooo    888  ooooo  888    888 oooo  oooo  oo oooooo      [white]o888     88  888ooooo     ooooooo[/]   oo ooo oooo   ooooooooo    oooooooo8  
 888ooo888   888oooo88   888ooo888      888oooo88    ooooo888   888    888  888o888     888oooo88   888   888   888   888     [white]888          888   888    ooooo888[/]   888 888 888   888    888 888ooooooo  
 888   888   888         888   888      888        888    888   888         8888 88o    888  88o    888   888   888   888     [white]888o     oo  888   888  888    888[/]   888 888 888   888    888         888 
o888o o888o o888o       o888o o888o    o888o        88ooo88 8o o888o       o888o o888o o888o  88o8   888o88 8o o888o o888o     [white]888oooo88  o888o o888o  88ooo88 8o[/] o888o888o888o  888ooo88   88oooooo88  
                                                                                                                                                                                o888                    
[/]");
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