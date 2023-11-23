﻿using Spectre.Console;

namespace HPH.ParkRunChamps.Cli.Pipeline; 

public class FilterNonMembersStep : IPipelineStep {
    public Task ExecuteStep(ParkRunChampsData data, IAnsiConsole console, StatusContext ctx)
    {
        // NOT IN USE
        // The names don't match between the PR results and the Members lst
        ctx.Status("Filtering Non-Member Results...");
        
        var memberNames = data.Members.Select(m => $"{m.FirstName} {m.Surname}".ToLower()).ToHashSet();
        var nonMembers = data.Results.Values.SelectMany(v => v).Where(m => !memberNames.Contains(m.Name.ToLower()));
        data.MembersOnlyResults = data.Results.ToDictionary(
            r => r.Key, 
            r => r.Value.Where(v => memberNames.Contains(v.Name.ToLower()))
        );
        
        var countOfAllResults = data.Results.Values.SelectMany(x => x).Count();
        var countOfMembersResults = data.MembersOnlyResults.Values.SelectMany(x => x).Count();
        
        console.MarkupLine($"[green]Filtered [bold]{countOfAllResults - countOfMembersResults}[/] non-members from [bold]{countOfAllResults}[/] results[/]");
        foreach (var parkRunResult in nonMembers)
        {
            console.MarkupLine("[red]Non-Member: [bold]{0}[/][/]", parkRunResult.Name);
        }
        
        return Task.CompletedTask;
    }
}