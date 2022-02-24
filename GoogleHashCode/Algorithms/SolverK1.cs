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

        foreach (var project in input.Projects)
        {
            var assignment = new Assignment();
            var notFount = false;
            foreach (var skill in project.Skills)
            {
                var contributor = input.Contributors
                    .Except(assignment.Contributors)
                    .FirstOrDefault(c => c.Skills.Any(r => r.Name == skill.Name && r.SkillLevel >= skill.SkillLevel));
                if (contributor is null)
                {
                    notFount = true;
                    continue;
                }

                assignment.Contributors.Add(contributor);
            }

            if (notFount)
                continue;
            assignment.Project = project;
            output.Assignments.Add(assignment);
        }

        return output;
    }
}