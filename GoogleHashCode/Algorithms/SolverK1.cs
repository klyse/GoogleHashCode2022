using System.Collections.Generic;
using System.Linq;
using GoogleHashCode.Base;
using GoogleHashCode.Model;

namespace GoogleHashCode.Algorithms;

public class SolverK1 : ISolver<Input, Output>
{
    public Output Solve(Input input)
    {
        var output = new Output();

        IDictionary<Contributor, int> blockedTill = input.Contributors.ToDictionary(c => c, _ => 0);

        foreach (var project in input.Projects
                     .OrderBy(c => c.BestBefore)
                     .ThenBy(c => c.Days)
                     .ThenByDescending(c => c.Score))
        {
            var assignment = new Assignment();
            var notFount = false;
            foreach (var skill in project.Skills)
            {
                var contributor = input.Contributors
                    .Except(assignment.Contributors)
                    .Where(c => c.Skills.Any(r => r.Name == skill.Name &&
                                                  (r.SkillLevel >= skill.SkillLevel ||
                                                   r.SkillLevel >= skill.SkillLevel - 1 &&
                                                   assignment.Contributors.Any(y => y.GetSkillLevel(skill) >= skill.SkillLevel))))
                    .OrderBy(c => blockedTill[c])
                    .ThenBy(c => c.Skills.Count)
                    .FirstOrDefault();

                if (contributor is null)
                {
                    notFount = true;
                    break;
                }

                blockedTill[contributor] += project.Days;
                assignment.Contributors.Add(contributor);
            }

            if (notFount)
                continue;
            
            assignment.Project = project;
            output.Assignments.Add(assignment);
            assignment.LevelUpContributors();
        }

        return output;
    }
}