using HPH.ParkRunChamps.Cli.Tests.TestClasses;
using Spectre.Console.Testing;

namespace HPH.ParkRunChamps.Cli.Tests;

[UsesVerify]
public class GenerateParkRunResultCommandApprovalTest {
    [Fact]
    public Task should_produce_expected_output()
    {
        
        var testConsole = new TestConsole();
        var parkRunDataService = new TestableHphBlogScraper();
        
        // var command = new GenerateParkRunResultCommand(testConsole, parkRunDataService);
        //
        // command.Execute();

        var output = testConsole.Output
            .NormalizeLineEndings();
        
        return Verify(output);
    }
}