#load "Utils.csx"
using System;

const int FLOOR_WIDTH = 101;
const int FLOOR_HEIGHT = 103;

class Robot
{
    public int X;
    public int Y;

    private int XVelocity;
    private int YVelocity;

    public Robot(int x, int y, int xVelocity, int yVelocity)
    {
        X = x;
        Y = y;
        XVelocity = xVelocity;
        YVelocity = yVelocity;
    }

    public void UpdateLocation()
    {
        // using positive modulo so it always wraps even for negatives
        X += XVelocity;
        X = (X % FLOOR_WIDTH + FLOOR_WIDTH) % FLOOR_WIDTH;

        Y += YVelocity;
        Y = (Y % FLOOR_HEIGHT + FLOOR_HEIGHT) % FLOOR_HEIGHT;
    }

    // qdrants NW = 0, SW = 1, NE = 2, SE = 3
    public int GetCurrentQuadrant()
    {
        if (X == FLOOR_WIDTH / 2 || Y == FLOOR_HEIGHT / 2)
        {
            return -1;
        }
        
        int qdrant = 0;
        if (X > FLOOR_WIDTH / 2)
        {
            qdrant += 2;
        }

        if (Y > FLOOR_HEIGHT / 2)
        {
            qdrant++;
        }

        return qdrant;
    }


    public static Robot CreateFromString(string input)
    {
        var parts = input.Split(" ");

        var coords = parts[0].Substring(2).Split(",").Select(int.Parse).ToArray();
        var velocity = parts[1].Substring(2).Split(",").Select(int.Parse).ToArray();

        return new Robot(coords[0], coords[1], velocity[0], velocity[1]);
    }

    public override string ToString()
    {
        return $"Robot: (X: {X}, Y: {Y}) - (xVel: {XVelocity}, yVel: {YVelocity})";
    }
}


var robots = Utils.FileToString("day14")
    .Split("\n")
    .Select(Robot.CreateFromString)
    .ToArray();

// simulate 100 seconds of robot movement
for (int i = 0; i < 100; i++)
{
    foreach (var robot in robots)
    {
        robot.UpdateLocation();
    }
}

int part1 = robots.Select(robot => robot.GetCurrentQuadrant())
    .Where(qdrant => qdrant != -1)
    .GroupBy(x => x)
    .Select(group => group.Count())
    .Aggregate(1, (acc, n) => acc * n);

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {-1}");
