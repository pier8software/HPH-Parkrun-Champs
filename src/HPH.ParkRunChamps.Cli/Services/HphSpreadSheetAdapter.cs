using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using HPH.ParkRunChamps.Cli.Pipeline;

namespace HPH.ParkRunChamps.Cli.Services;

public class HphSpreadSheetAdapter {
    public async Task<IEnumerable<ClubMember>> GetMembersList()
    {
        using var service = await GetSheetsService();
        var getRequest = service.Spreadsheets.Values.Get("1G3iQNkVYrtL36UnHYiZFeLpdtmzcTioJGkdvymXSYY0", "A:E");
        var data = await getRequest.ExecuteAsync();
        return data.Values.Select(row => new ClubMember(row[0].ToString(), row[1].ToString(), row[3].ToString()));
    }

    public async Task<string> GetParkRunOfTheMonth(int month)
    {
        var (colChar, _) = MapMonthToColumnIndex(month);
        using var service = await GetSheetsService();
        var getRequest = service.Spreadsheets.Values.Get("1pbyEGcux3ResvlU4bXG5zuF39sZ0mOhJmD2e48Jp3pc", $"Womens parkrun!{colChar}2");
        var valueRange = await getRequest.ExecuteAsync();
        return valueRange.Values[0][0].ToString();
    }

    public async Task<IEnumerable<ClubChampResult>> GetClubChampsForTheMonthAndGender(int month, string gender)
    {
        var (_, colIdx) = MapMonthToColumnIndex(month);
        using var service = await GetSheetsService();
        var getRequest = service.Spreadsheets.Values.Get("1pbyEGcux3ResvlU4bXG5zuF39sZ0mOhJmD2e48Jp3pc", $"{gender}s parkrun!A3:AA");
        var data = await getRequest.ExecuteAsync();

        var results = new List<ClubChampResult>();
        for (var i = 0; i < data.Values.Count; i++)
        {
            var row = data.Values[i];
            results.Add(new ClubChampResult(row[0].ToString(), TrimAndConvert(row[3]), TrimAndConvert(row[colIdx]), i));
        }

        return results;

        double TrimAndConvert(object value)
        {
            return Convert.ToDouble(value.ToString().TrimEnd('%'));
        }
    }

    public async Task UpdateClubChampsWhmResult(ClubChampResult clubChampResult, string gender)
    {
        var range = $"{gender}s parkrun!D{clubChampResult.RowIndex}";
        await UpdateClubChampResult(clubChampResult.WhmAgeGrade, range);
    }

    public async Task UpdateClubChampsMonthlyResult(ClubChampResult clubChampResult, string gender, int month)
    {
        var (colChar, _) = MapMonthToColumnIndex(month);
        var range = $"{gender}s parkrun!{colChar}{clubChampResult.RowIndex}";
        await UpdateClubChampResult(clubChampResult.MonthAgeGrade, range);
    }

    private async Task<SheetsService> GetSheetsService()
    {
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        using var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read);
        var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            scopes,
            "user", CancellationToken.None);

        return new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = userCredential
        });
    }

    private static (string, int) MapMonthToColumnIndex(int month)
    {
        return month switch
        {
            1 => ("X", 23), // jan
            2 => ("Z", 25), // feb
            3 => ("", 1), // mar
            4 => ("F", 5), // apl
            5 => ("H", 7), // may
            6 => ("J", 9), // jun
            7 => ("L", 11), // jul
            8 => ("N", 13), // aug
            9 => ("P", 15), // sep
            10 => ("R", 17), // oct
            11 => ("T", 19), // nov
            12 => ("V", 21), // dec
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };
    }

    private async Task UpdateClubChampResult(double ageGrade, string range)
    {
        var valueRange = new ValueRange
        {
            MajorDimension = "COLUMNS",
            Values = new List<IList<object>> { new List<object> { ageGrade } }
        };
        
        using var service = await GetSheetsService();
        service.Spreadsheets.Values.Update(valueRange, "1pbyEGcux3ResvlU4bXG5zuF39sZ0mOhJmD2e48Jp3pc", range);
    }
}