using HPH.ParkRunChamps.Cli.Pipeline;
using HPH.ParkRunChamps.Cli.Pipeline.Steps;
using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

var hphBlogScraper = new HphBlogScraper();
var googleSheetsAdapter = new HphSpreadSheetAdapter();

var parkRunChampsData = new ParkRunChampsData();
var parkRunChampsPipeline = new ParkRunChampsPipeline();

parkRunChampsPipeline.AddStep(new GetLatestParkRunInfoStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetResultsStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetMembersStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new GetParkRunOfTheMonthStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new GetClubChampsResultsStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new UpdateClubChampsStep(googleSheetsAdapter));

await parkRunChampsPipeline.Execute(parkRunChampsData, AnsiConsole.Console);