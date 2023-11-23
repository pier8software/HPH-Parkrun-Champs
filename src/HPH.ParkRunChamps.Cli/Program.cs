using HPH.ParkRunChamps.Cli;
using HPH.ParkRunChamps.Cli.Pipeline;
using Spectre.Console;

var hphBlogScraper = new HphBlogScraper();
var googleSheetsAdapter = new GoogleSheetAdapter();

var parkRunChampsData = new ParkRunChampsData();
var parkRunChampsPipeline = new ParkRunChampsPipeline();

parkRunChampsPipeline.AddStep(new GetLatestParkRunInfoStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetResultsStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetMembersStep(googleSheetsAdapter));

await parkRunChampsPipeline.Execute(parkRunChampsData, AnsiConsole.Console);