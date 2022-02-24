using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model;

public class Contributor : IEqualityComparer<Contributor>
{
    public string Name { get; init; }
    public IReadOnlyList<Skill> Skills => _SkillList;

    private List<Skill> _SkillList;

    private Dictionary<string, Skill> _SkillIndex;

    public Skill GetSkill(string skill)
    {
        if (_SkillIndex.ContainsKey(skill))
            return _SkillIndex[skill];

        var x = _SkillIndex.Values;

        return null;
    }

    public Contributor(string name, List<Skill> skills)
    {
        Name = name;
        _SkillIndex = skills.ToDictionary(q => q.Name, q => q);
        _SkillList = skills;
    }

    public bool Equals(Contributor x, Contributor y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(Contributor obj)
    {
        return obj.Name.GetHashCode();
    }

    public Skill GetSkill(Skill skill) => GetSkill(skill.Name);
    public int GetSkillLevel(Skill skill) => GetSkill(skill.Name)?.SkillLevel ?? 0;

    public void LevelUp(Skill skill)
    {
        var current = GetSkill(skill.Name);
        if (current == null)
        {
            current = new Skill { Name = skill.Name, SkillLevel = 0 };
            _SkillIndex[current.Name] = current;
            _SkillList.Add(current);
        }

        current.SkillLevel++;
    }
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
