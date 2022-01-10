using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleHashCode.Model;

public record Street(int BeginIntersectionID, int EndIntersectionID, string StreetName, int Length);

public record CarPath(int NumberOfStreets, List<string> StreetNames);

public record Input(int Duration, int NumberOfIntersections, int NumberOfStreets, int NumberOfCars, int BonusPoints, List<Street> Streets, List<CarPath> CarPaths)
{
    private static Street ParseStreet(string line)
    {
        var list = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        return new Street(int.Parse(list[0]), int.Parse(list[1]), list[2], int.Parse(list[3]));
    }

    private static CarPath ParseCarPath(string line)
    {
        var list = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        return new CarPath(int.Parse(list[0]), list.Skip(1).ToList());
    }

    public static Input Parse(string[] values)
    {
        var first = values.First().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        return new Input(first[0], first[1], first[2], first[3], first[4], values.Skip(1).Take(first[2]).Select(ParseStreet).ToList(), values.Skip(1 + first[2]).Select(ParseCarPath).ToList());
    }
}