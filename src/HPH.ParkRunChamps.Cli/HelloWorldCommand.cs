using Spectre.Console;

namespace HPH.ParkRunChamps.Cli; 

public class HelloWorldCommand(IAnsiConsole console) {
    public void Execute()
    {
        console.Markup("[underline red]Hello[/] World!");
    }
}