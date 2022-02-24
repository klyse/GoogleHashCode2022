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

        var row = 1;
        for (var i = 0; i < contributors; i++)
        {
            var splitRow = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var name = splitRow[0];
            var count = int.Parse(splitRow[1]);

            var skillsList = new List<Skills>();
            row += 1;
            for (var y = 0; y < count; y++)
            {
                var splitRow1 = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var skillName = splitRow1[0];
                var level = int.Parse(splitRow1[1]);
                skillsList.Add(new Skills(skillName, level));
                row += 1;
            }
            contributorsList.Add(new Contributor(name, skillsList));
        }
        
        for (var i = 0; i < projects; i++)
        {
            var splitRow = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var name = splitRow[0];
            var numDays = int.Parse(splitRow[1]);
            var score = int.Parse(splitRow[2]);
            var bestBefore = int.Parse(splitRow[3]);
            var roles = int.Parse(splitRow[4]);

            var skillsList = new List<Skills>();
            row += 1;
            for (var y = 0; y < roles; y++)
            {
                var splitRow1 = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var skillName = splitRow1[0];
                var level = int.Parse(splitRow1[1]);
                skillsList.Add(new Skills(skillName, level));
                row += 1;
            }
            projectsList.Add(new Project(name, numDays, score, bestBefore, skillsList));
        }

        return new Input(contributorsList, projectsList);
    }
}