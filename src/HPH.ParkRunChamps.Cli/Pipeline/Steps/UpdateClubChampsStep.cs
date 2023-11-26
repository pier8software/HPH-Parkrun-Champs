using HPH.ParkRunChamps.Cli.Services;
using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline.Steps;

internal class UpdateClubChampsStep(HphSpreadSheetAdapter hphSpreadSheetsAdapter) : IPipelineStep {
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
            UpdateWhmResults(data, gender, newChamps, whmParkRunResults);
            
            ctx.Status($"Updating {gender}s {data.ParkRunOfTheMonth} results...");
            var parkRunOfTheMonthResults = results.Where(x => x.ParkRun.Equals(data.ParkRunOfTheMonth));
            UpdateParkRunOfTheMonthResults(data, gender, newChamps, parkRunOfTheMonthResults);
        }
    }

    private void UpdateParkRunOfTheMonthResults(ParkRunChampsData data, string gender, List<ParkRunResult> newChamps, IEnumerable<ParkRunResult> parkRunOfTheMonthResults)
    {
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
                hphSpreadSheetsAdapter.UpdateClubChampsMonthlyResult(clubChampResult, gender, data.LastParkRunDate.Month);
            }
        }
    }

    private void UpdateWhmResults(ParkRunChampsData data, string gender, List<ParkRunResult> newChamps, IEnumerable<ParkRunResult> whmParkRunResults)
    {
        foreach (var parkRunResult in whmParkRunResults)
        {
            var clubChampResult = data.ClubChampResults[gender].FirstOrDefault(x => x.Name.Equals(parkRunResult.Name));
            if (clubChampResult is null)
            {
                newChamps.Add(parkRunResult);
                continue;
            }

            if (parkRunResult.AgeGrade > clubChampResult.WhmAgeGrade)
            {
                clubChampResult = clubChampResult with
                {
                    WhmAgeGrade = parkRunResult.AgeGrade
                };
                hphSpreadSheetsAdapter.UpdateClubChampsWhmResult(clubChampResult, gender);
            }
        }
    }
}