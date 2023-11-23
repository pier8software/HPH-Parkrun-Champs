using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using HPH.ParkRunChamps.Cli.Pipeline;

namespace HPH.ParkRunChamps.Cli;

public class GoogleSheetAdapter {
    public async Task<IEnumerable<ClubMember>> GetMembersList()
    {
        using var service = await GetSheetsService();
        var batchGet = service.Spreadsheets.Values.Get("1G3iQNkVYrtL36UnHYiZFeLpdtmzcTioJGkdvymXSYY0", "A:B");
        var data = await batchGet.ExecuteAsync();
        return data.Values.Select(row => new ClubMember(row[0].ToString(), row[1].ToString(), row[3].ToString()));
    }

    private async Task<SheetsService> GetSheetsService()
    {
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
        {
            var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                scopes,
                "user", CancellationToken.None);

            return new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = userCredential
            });
        }
    }
}