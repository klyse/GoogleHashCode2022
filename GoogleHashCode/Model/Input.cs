using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model;

public record Contributor(string Name, List<Skills> Skills);
public record Project(string Name, int Days, int Score, int BestBefore, List<Skills> Skills);
public record Skills(string Name, int SkillLevel);

public record Input(List<Contributor> Contributors, List<Project> Projects)
{
    public static Input Parse(string[] values)
    {
        var first = values.First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var contributors = first[0];
        var projects = first[1];

        var cnt = 0;
        foreach (var row in values.Skip(1))
        {
            var splitRow = row.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var name = splitRow[0];
            var count = int.Parse(splitRow[1]);

            cnt++;
        }
    }
}