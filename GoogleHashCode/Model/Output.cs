using System.Collections.Generic;
using GoogleHashCode.Base;
using System.Linq;

namespace GoogleHashCode.Model;

public record Assignment(Project Project, List<Contributor> Contributors);


public record Output(List<Assignment> Assignments) : IOutput
{
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