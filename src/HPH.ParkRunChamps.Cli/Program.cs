using HPH.ParkRunChamps.Cli;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var services = new ServiceCollection();
services.AddSingleton(AnsiConsole.Console);
services.AddSingleton<GenerateParkRunResultCommand>();
services.AddSingleton<IParkRunDataService, ParkRunDataService>();

var provider = services.BuildServiceProvider();
var command = provider.GetRequiredService<GenerateParkRunResultCommand>();

await command.Execute();