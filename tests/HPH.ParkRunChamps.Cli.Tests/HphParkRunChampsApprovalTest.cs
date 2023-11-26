using HPH.ParkRunChamps.Cli.Pipeline;
using HPH.ParkRunChamps.Cli.Pipeline.Steps;
using HPH.ParkRunChamps.Cli.Tests.TestClasses;
using Spectre.Console.Testing;

namespace HPH.ParkRunChamps.Cli.Tests;

[UsesVerify]
public class HphParkRunChampsApprovalTest {
    [Fact]
    public Task should_produce_expected_output()
    {
        var testConsole = new TestConsole();
        var scraper = new TestableHphBlogScraper();
        var spreadSheetAdapter = new TestableSpreadSheetAdapter();

        var pipeline = new ParkRunChampsPipeline();
        pipeline.AddStep(new GetLatestParkRunInfoStep(scraper));
        pipeline.AddStep(new GetResultsStep(scraper));
        pipeline.AddStep(new GetMembersStep(spreadSheetAdapter));
        pipeline.AddStep(new GetParkRunOfTheMonthStep(spreadSheetAdapter));
        pipeline.AddStep(new GetClubChampsResultsStep(spreadSheetAdapter));
        pipeline.AddStep(new UpdateClubChampsStep(spreadSheetAdapter));

        pipeline.Execute(new ParkRunChampsData(), testConsole);

        var output = testConsole.Output
            .NormalizeLineEndings();

        return Verify(output);
    }
}