using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model;

public record Contributor(string Name, List<Skills> Skills);

public record Skills(string Name, int SkillLevel);

public record Input(List<Contributor> Contributors)
{
    public static Input Parse(string[] values)
    {
        var first = values.First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var contributors = first.First();
        var projects = first.Skip(1).First();
        
        foreach (var row in values.Skip(1))
        {
            var splitRow = row.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var name = splitRow[0];
            var count = int.Parse(splitRow[1]);
            
            
        }
    }
}