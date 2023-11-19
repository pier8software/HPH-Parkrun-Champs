using HPH.ParkRunChamps.Cli;
using HPH.ParkRunChamps.Cli.Pipeline;
using Spectre.Console;

AnsiConsole.Console.Write(
    new FigletText("HPH PARKRUN CHAMPS")
        .LeftJustified()
        .Color(Color.Red));

var parkRunChampsData = new ParkRunChampsData();
var parkRunChampsPipeline = new ParkRunChampsPipeline();

parkRunChampsPipeline.AddStep(new GetLatestParkRunInfoStep(new HphBlogScraper()));
parkRunChampsPipeline.AddStep(new GetResultsStep(new HphBlogScraper()));

await AnsiConsole.Console.Status()
    .Spinner(Spinner.Known.Dots)
    .StartAsync("Updating ParkRun Champs...", async ctx =>
    {
        await parkRunChampsPipeline.Execute(parkRunChampsData, AnsiConsole.Console, ctx);
    });