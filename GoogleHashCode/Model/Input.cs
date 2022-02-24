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

        var contributorsList = new List<Contributor>();
        var projectsList = new List<Project>();

        for (var i = 1; i < contributors; )
        {
            var splitRow = values[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var name = splitRow[0];
            var count = int.Parse(splitRow[1]);

            var skillsList = new List<Skills>();
            for (var y = 0; y < count;i++, y++)
            {
                var splitRow1 = values[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var skillName = splitRow1[0];
                var level = int.Parse(splitRow1[1]);
                skillsList.Add(new Skills(skillName, level));
            }
            contributorsList.Add(new Contributor(name, skillsList));
        }

        return new Input(contributorsList, projectsList);
    }
}