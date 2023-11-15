using Spectre.Console.Testing;
using Shouldly;

namespace HPH.ParkRunChamps.Cli.Tests;

public class HelloWorldTest {
    [Fact]
    public void should_output_hello_world()
    {
        var testConsole = new TestConsole();
        new HelloWorldCommand(testConsole).Execute();
        
        testConsole.Output
            .NormalizeLineEndings()
            .ShouldBe("Hello World!");
    }
}