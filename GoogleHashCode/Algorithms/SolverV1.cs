using System.Collections.Generic;
using System.Linq;
using GoogleHashCode.Base;
using GoogleHashCode.Model;

namespace GoogleHashCode.Algorithms;

public class SolverV1 : ISolver<Input, Output>
{
    Output output = new();

    public Output Solve(Input input)
    {
        var projects = input.Projects.OrderBy(q => q.BestBefore).ThenByDescending(q => q.Score).ToList();
        var contributors = input.Contributors.ToList();
        while (projects.Count > 0)
        {
            var skippedProjects = new List<Project>();
            var assigned = false;
            foreach (var project in projects)
            {
                var assignment = new Assignment(project);
                foreach (var skill in project.Skills)
                {
                    var contrib = contributors.FirstOrDefault(q => assignment.SkillMeet(skill, q));
                    if (contrib != null)
                    {
                        assignment.Contributors.Add(contrib);
                        contributors.Remove(contrib);
                    }
                    else
                        break;
                }

                if (assignment.AllSkillsAssigned)
                {
                    output.Assignments.Add(assignment);
                    assignment.LevelUpContributors();
                    assigned = true;
                }
                else
                    skippedProjects.Add(project);

                if (contributors.Count == 0)
                    break;
            }

            if (skippedProjects.Count > 0 && assigned)
            {
                projects = skippedProjects;
                contributors = input.Contributors.ToList();
            }
            else
                break;
        }
        return output;
    }
}