namespace Tests;

public class VolkmarsTest
{
    [Test]
    [Parallelizable(ParallelScope.All)]
    [TestCase("a_an_example.in.txt")]
    [TestCase("b_better_start_small.in.txt")]
    [TestCase("c_collaboration.in.txt")]
    [TestCase("d_dense_schedule.in.txt")]
    [TestCase("e_exceptional_skills.in.txt")]
    [TestCase("f_find_great_mentors.in.txt")]
    public void Solver2(string example)
    {
        var content = example.ReadFromFile();
        var solver = new SolverV1();
        var input = Input.Parse(content);
        var output = solver.Solve(input);

        Console.WriteLine($"Total Score: {output.GetScore(input):N0}");
        example.WriteToFile(output.GetOutputFormat());
        Assert.Pass();
    }
}