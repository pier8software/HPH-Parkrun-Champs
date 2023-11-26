using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

public class UpdateClubChampsStep(IHphSpreadSheetAdapter hphSpreadSheetsAdapter) : IPipelineStep {
    private const string WHM = "Woodhouse Moor";

    public async Task ExecuteStep(ParkRunChampsData data,
        IAnsiConsole console,
        StatusContext ctx)
    {
        var newChamps = new List<ParkRunResult>();
        foreach (var (gender, results) in data.ParkRunResults)
        {
            ctx.Status($"Updating {gender}s {WHM} results...");
            var whmParkRunResults = results.Where(x => x.ParkRun.Equals(WHM));
            var updatedResults = await UpdateWhmResults(data, gender, newChamps, whmParkRunResults);
            
            
            ctx.Status($"Updating {gender}s {data.ParkRunOfTheMonth} results...");
            var parkRunOfTheMonthResults = data.ParkRunOfTheMonth.Equals("Any!") 
                ? results.Where(x=> !x.ParkRun.Equals(WHM))
                : results.Where(x => x.ParkRun.Equals(data.ParkRunOfTheMonth));
            updatedResults += await UpdateParkRunOfTheMonthResults(data, gender, newChamps, parkRunOfTheMonthResults);
            console.MarkupLine("[green]Updated [bold]{0} {1}s[/] results[/]", updatedResults, gender);
        }
        
        if (newChamps.Any())
        {
            console.MarkupLine("[red]New ParkRun Results found[/]");
            foreach (var (name, parkRun, ageGrade) in newChamps)
            {
                console.MarkupLine("[grey]Name: [bold]{0}[/]\t\tParkRun: [bold]{1}[/]\t\tAgeGrade: [bold]{2}[/][/]", name, parkRun, ageGrade);
            }
        }
    }

    private async Task<int> UpdateParkRunOfTheMonthResults(ParkRunChampsData data, string gender, List<ParkRunResult> newChamps, IEnumerable<ParkRunResult> parkRunOfTheMonthResults)
    {
        var noOfUpdatedResults = 0;
        foreach (var parkRunResult in parkRunOfTheMonthResults)
        {
            var clubChampResult = data.ClubChampResults[gender].FirstOrDefault(x => x.Name.Equals(parkRunResult.Name));
            if (clubChampResult is null)
            {
                newChamps.Add(parkRunResult);
                continue;
            }

            if (parkRunResult.AgeGrade > clubChampResult.MonthAgeGrade)
            {
                clubChampResult = clubChampResult with
                {
                    MonthAgeGrade = parkRunResult.AgeGrade
                };
                await hphSpreadSheetsAdapter.UpdateClubChampsMonthlyResult(clubChampResult, gender, data.LastParkRunDate.Month);
                noOfUpdatedResults++;
            }
        }

        return noOfUpdatedResults;
    }

    private async Task<int> UpdateWhmResults(ParkRunChampsData data, string gender, List<ParkRunResult> newChamps, IEnumerable<ParkRunResult> whmParkRunResults)
    {
        var noOfUpdatedResults = 0;
        foreach (var parkRunResult in whmParkRunResults)
        {
            var clubChampResult = data.ClubChampResults[gender].FirstOrDefault(x => x.Name.Equals(parkRunResult.Name));
            if (clubChampResult is null)
            {
                newChamps.Add(parkRunResult);
            }
            else if (parkRunResult.AgeGrade > clubChampResult.WhmAgeGrade)
            {
                clubChampResult = clubChampResult with
                {
                    WhmAgeGrade = parkRunResult.AgeGrade
                };
                await hphSpreadSheetsAdapter.UpdateClubChampsWhmResult(clubChampResult, gender);
                noOfUpdatedResults++;
            }
        }
        return noOfUpdatedResults;
    }
}