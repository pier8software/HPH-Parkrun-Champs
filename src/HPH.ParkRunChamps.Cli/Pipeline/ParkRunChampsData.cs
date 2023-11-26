namespace HPH.ParkRunChamps.Cli.Pipeline;

public class ParkRunChampsData 
{
    public DateOnly LastParkRunDate { get; set; }
    public string ParkRunOfTheMonth { get; set; }
    public Uri ResultsLink { get; set; }
    public IDictionary<string, IEnumerable<ParkRunResult>> ParkRunResults { get; set; }
    
    public IEnumerable<ClubMember> Members { get; set; }
    public Dictionary<string, IEnumerable<ParkRunResult>> MembersOnlyResults { get; set; }
    
    public Dictionary<string, IEnumerable<ClubChampResult>> ClubChampResults { get; set; }
}

public record ParkRunResult(string Name, string ParkRun, double AgeGrade);

public record ClubMember(string Surname, string FirstName, string Gender);

public record ClubChampResult(string Name, double WhmAgeGrade, double MonthAgeGrade, int RowIndex);