using System.Collections.Generic;
using GoogleHashCode.Base;
using System.Linq;

namespace GoogleHashCode.Model;

public class Assignment
{
    public Project Project { get; set; }
    public  List<Contributor> Contributors { get; set; } = new();

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