using Spectre.Console;

namespace HPH.ParkRunChamps.Cli; 

public class GenerateParkRunResultCommand(IAnsiConsole console, IParkRunDataService parkRunData) {
    public async Task Execute()
    {
        console.Write(
            new FigletText("HPH ParkRun Champs")
                .LeftJustified()
                .Color(Color.Red));
        
        // Get latest PR date
        var lastParkRunDate = await parkRunData.GetLatestParkRunDate();
        
        // Confirm to Continue
        console.Confirm($"Would you like to update the ParkRun Champs results for {lastParkRunDate:d}?");
        
        // Status
            // Get Members
            // Get Results
            // Update Results
            
        // Display Top 5 Women
        // Display Top 5 Men

        console.Write("Press any key to exit...");
        Console.ReadKey();
    }
}

public interface IParkRunDataService {
    Task<DateTime> GetLatestParkRunDate();
}

public class ParkRunDataService : IParkRunDataService {
    public async Task<DateTime> GetLatestParkRunDate() {
        return DateTime.Now;
    }
}