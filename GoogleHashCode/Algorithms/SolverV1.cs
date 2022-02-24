namespace GoogleHashCode.Algorithms;

public class SolverV1 : ISolver<Input, Output>
{
    Output Output = new();

    IDictionary<Contributor, int> BlockedTill;
    Input Input;

    Dictionary<string, List<Contributor>> SkillIndex;


    List<Project> GetProjectsSorted(List<Project> projects)
    {

        return projects.OrderBy(q => q.BestBefore).ThenBy(q => q.Days).ThenByDescending(q => q.Score).ToList();
    }

    Contributor FindContributor(Assignment assignment, Skill requiredSkill)
    {
        if (!SkillIndex.TryGetValue(requiredSkill.Name, out var list))
            return null;

        var isMentorAvailable = assignment.MentorAvailable(requiredSkill);
        var requiredSkillLevel = isMentorAvailable ? requiredSkill.SkillLevel - 1 : requiredSkill.SkillLevel;


        var candidateInfo = SkillIndex[requiredSkill.Name]
                .Select(q => new { candidate = q, level = q.GetSkillLevel(requiredSkill) })
                .Where(q => q.level >= requiredSkillLevel && !assignment.Contributors.Any(s => s == q.candidate))
                .OrderBy(q => BlockedTill[q.candidate]).ThenBy( q => q.level).ThenBy(q => q.candidate.Skills.Count)
                .FirstOrDefault();
        if (candidateInfo != null)
            return candidateInfo?.candidate;

        if (requiredSkill.SkillLevel == 1 && isMentorAvailable)
            return Input.Contributors
                .Where(q => q.GetSkillLevel(requiredSkill) == 0 && !assignment.Contributors.Any(s => s == q))
                .OrderBy(q => BlockedTill[q])
                .FirstOrDefault();

        return null;
    }

    void UpdateBlockedTill(Assignment assignment)
    {
        foreach (var contrib in assignment.Contributors)
            BlockedTill[contrib] += assignment.Project.Days;
    }

    List<Contributor> GetContribs(Skill skill)
    {
        if (!SkillIndex.ContainsKey(skill.Name))
            SkillIndex[skill.Name] = new();
        return SkillIndex[skill.Name];
    }

    void UpdateSkillIndex()
    {
        SkillIndex = new();
        foreach (var contrib in Input.Contributors)
            foreach (var skill in contrib.Skills)
                GetContribs(skill).Add(contrib);
    }

    void Init(Input input)
    {
        Input = input;
        UpdateSkillIndex();
    }

    public Output Solve(Input input)
    {
        Init(input);

        var projects = GetProjectsSorted(input.Projects);
        BlockedTill = input.Contributors.ToDictionary(c => c, _ => 0);

        while (projects.Count > 0)
        {
            var skippedProjects = new List<Project>();
            var assigned = false;
            foreach (var project in projects)
            {
                var assignment = new Assignment(project);
                foreach (var skill in project.Skills)
                {
                    var contrib = FindContributor(assignment, skill);
                    if (contrib != null)
                        assignment.Contributors.Add(contrib);
                    else
                        break;
                }

                if (assignment.AllSkillsAssigned)
                {
                    Output.Assignments.Add(assignment);
                    if (assignment.LevelUpContributors())
                        UpdateSkillIndex();

                    UpdateBlockedTill(assignment);
                    assigned = true;
                }
                else
                    skippedProjects.Add(project);
            }

            if (skippedProjects.Count > 0 && assigned)
                projects = GetProjectsSorted(skippedProjects);
            else
                break;
        }
        return Output;
    }
}