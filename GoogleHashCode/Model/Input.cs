using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model;

public record Contributor(string Name, List<Skill> Skills)
{
    private Dictionary<string, Skill> _Skill = Skills.ToDictionary(q => q.Name, q => q);

    public Skill GetSkill(string skill)
    {
        if (_Skill.ContainsKey(skill))
            return _Skill[skill];
        return null;
    }

    public Skill GetSkill(Skill skill) => GetSkill(skill.Name);
    public int GetSkillLevel(Skill skill) => GetSkill(skill.Name)?.SkillLevel ?? 0;
}
public record Project(string Name, int Days, int Score, int BestBefore, List<Skill> Skills);

public class Skill
{
    public string Name { get; init; }
    public int SkillLevel { get; set; }

}

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

            var skillsList = new List<Skill>();
            row += 1;
            for (var y = 0; y < count; y++)
            {
                var splitRow1 = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var skillName = splitRow1[0];
                var level = int.Parse(splitRow1[1]);
                skillsList.Add(new Skill { Name = skillName, SkillLevel = level });
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

            var skillsList = new List<Skill>();
            row += 1;
            for (var y = 0; y < roles; y++)
            {
                var splitRow1 = values[row].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var skillName = splitRow1[0];
                var level = int.Parse(splitRow1[1]);
                skillsList.Add(new Skill { Name = skillName, SkillLevel = level });
                row += 1;
            }
            projectsList.Add(new Project(name, numDays, score, bestBefore, skillsList));
        }

        return new Input(contributorsList, projectsList);
    }
}