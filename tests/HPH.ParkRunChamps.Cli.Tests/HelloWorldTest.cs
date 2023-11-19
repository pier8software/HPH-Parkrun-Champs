using Spectre.Console.Testing;
using Shouldly;

namespace HPH.ParkRunChamps.Cli.Tests;

[UsesVerify]
public class HelloWorldTest {
    [Fact]
    public Task should_output_hello_world()
    {
        var testConsole = new TestConsole();
        new HelloWorldCommand(testConsole).Execute();

        var output = testConsole.Output
            .NormalizeLineEndings();
        
        return Verify(output);
    }
}