using System.Collections.Generic;
using GoogleHashCode.Base;
using System.Linq;
using System;

namespace GoogleHashCode.Model;

public class Assignment
{
    public Project Project { get; set; }
    public List<Contributor> Contributors { get; set; } = new();

    public bool AllSkillsAssigned => Project.Skills.Count == Contributors.Count;

    public bool SkillMeet(Skill requiredSkill, Contributor contributor)
    {
        var skillLevel = contributor.GetSkillLevel(requiredSkill);
        if (skillLevel >= requiredSkill.SkillLevel)
            return true;

        return (skillLevel == requiredSkill.SkillLevel - 1) && Contributors.Any(q => q.GetSkillLevel(requiredSkill) >= requiredSkill.SkillLevel);
    }

    public void LevelUpContributors()
    {
        if (!AllSkillsAssigned)
            throw new Exception("Not all assigned");

        for (int i = 0; i < Project.Skills.Count; i++)
        {
            var skill = Project.Skills[i];
            var contrib = Contributors[i];
            if (contrib.GetSkillLevel(skill) <= skill.SkillLevel)
                contrib.LevelUp(skill);
        }
    }

    public Assignment() { }
    public Assignment(Project project)
    {
        Project = project;
    }
}

public class Output : IOutput
{
    public List<Assignment> Assignments { get; set; } = new();

    public string[] GetOutputFormat()
    {
        var output = new List<string>();
        output.Add(Assignments.Count.ToString());
        foreach (var assignment in Assignments)
        {
            output.Add(assignment.Project.Name);
            output.Add(string.Join(" ", assignment.Contributors.Select(q => q.Name)));
        }

        return output.ToArray();
    }

    public int GetScore(Input input)
    {
        return 0;
    }
}