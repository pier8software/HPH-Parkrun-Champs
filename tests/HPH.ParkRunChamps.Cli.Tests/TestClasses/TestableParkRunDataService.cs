namespace HPH.ParkRunChamps.Cli.Tests.TestClasses; 

public class TestableParkRunDataService : IParkRunDataService {
    public Task<DateTime> GetLatestParkRunDate()
    {
        return Task.FromResult(new DateTime(1985, 10, 26));
    }
}