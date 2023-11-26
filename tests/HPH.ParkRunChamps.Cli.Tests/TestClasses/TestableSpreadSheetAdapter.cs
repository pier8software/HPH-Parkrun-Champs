using HPH.ParkRunChamps.Cli.Pipeline;
using HPH.ParkRunChamps.Cli.Services;

namespace HPH.ParkRunChamps.Cli.Tests.TestClasses; 

public class TestableSpreadSheetAdapter : IHphSpreadSheetAdapter {
    public Task<IEnumerable<ClubMember>> GetMembersList()
    {
        return Task.FromResult(Enumerable.Empty<ClubMember>());
    }

    public Task<string> GetParkRunOfTheMonth(int month)
    {
        return Task.FromResult(string.Empty);
    }

    public Task<IEnumerable<ClubChampResult>> GetClubChampsForTheMonthAndGender(int month, string gender)
    {
        return Task.FromResult(Enumerable.Empty<ClubChampResult>());
    }

    public Task UpdateClubChampsWhmResult(ClubChampResult clubChampResult, string gender)
    {
        return Task.CompletedTask;
    }

    public Task UpdateClubChampsMonthlyResult(ClubChampResult clubChampResult, string gender, int month)
    {
        return Task.CompletedTask;
    }
}