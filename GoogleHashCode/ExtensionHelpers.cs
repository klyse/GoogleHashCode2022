namespace GoogleHashCode;

public static class ExtensionHelpers
{
    public static string[] ReadFromFile(this string fileName)
    {
        var readAllLines = File.ReadAllLines(Path.Combine(EnvironmentConstants.InputPath, fileName));
        return readAllLines;
    }

    public static void WriteToFile(this string fileName, string[] lines)
    {
        File.WriteAllLines(Path.Combine(EnvironmentConstants.OutputPath, fileName), lines);
    }

    public static void WriteToFile(this string fileName, string line)
    {
        WriteToFile(fileName, new List<string> { line }.ToArray());
    }

    public static bool HasSkill(this Contributor contributor, Skill skill)
    {
        if (skill.SkillLevel == 0)
            return true;

        return contributor.Skills.Any(q => q.Name == skill.Name && q.SkillLevel >= skill.SkillLevel);
    }

}