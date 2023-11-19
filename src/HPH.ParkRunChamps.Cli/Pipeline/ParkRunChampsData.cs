namespace HPH.ParkRunChamps.Cli.Pipeline;

public class ParkRunChampsData 
{
    public DateOnly LastParkRunDate { get; set; }
    public Uri ResultsLink { get; set; }
    public IDictionary<string, IEnumerable<ParkRunResult>> Results { get; set; }
}

public record ParkRunResult(string Name, string ParkRun, string AgeGrade);