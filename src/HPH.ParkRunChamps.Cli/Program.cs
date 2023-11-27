using HPH.ParkRunChamps.Cli;
using HPH.ParkRunChamps.Cli.Pipeline;
using HPH.ParkRunChamps.Cli.Pipeline.Steps;
using HPH.ParkRunChamps.Cli.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

var parkRunChampsConfiguration = configuration.Get<HphParkRunChampsConfiguration>();
configuration.GetSection("installed").Bind(parkRunChampsConfiguration);

var hphParkRunChampsConfiguration = new HphParkRunChampsConfiguration();
configuration.Bind(hphParkRunChampsConfiguration);

var gSheetsConfig = new GSheetsConfig();
configuration.GetSection("installed").Bind(gSheetsConfig);

hphParkRunChampsConfiguration.Installed = gSheetsConfig;

var hphBlogScraper = new HphBlogScraper();
var googleSheetsAdapter = new HphSpreadSheetAdapter(hphParkRunChampsConfiguration);

var parkRunChampsData = new ParkRunChampsData();
var parkRunChampsPipeline = new ParkRunChampsPipeline();

parkRunChampsPipeline.AddStep(new GetLatestParkRunInfoStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetResultsStep(hphBlogScraper));
parkRunChampsPipeline.AddStep(new GetMembersStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new GetParkRunOfTheMonthStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new GetClubChampsResultsStep(googleSheetsAdapter));
parkRunChampsPipeline.AddStep(new UpdateClubChampsStep(googleSheetsAdapter));

await parkRunChampsPipeline.Execute(parkRunChampsData, AnsiConsole.Console);