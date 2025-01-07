#load "Utils.csx"
using System;
using System.Threading;

const char ROBOT = '@';
const char WALL = '#';
const char BOX = 'O';
const char BOX_LEFT = '[';
const char BOX_RIGHT = ']';
const char EMPTY = '.';

(int, int) PositionChange(char c) => c switch 
{
    '<' => (-1, 0),
    '>' => (1, 0),
    '^' => (0, -1),
    'v' => (0, 1),
    _ => throw new Exception("BAD POSITION GIVEN")
};

int CalculateGPSSum(char[][] grid, char boxChar)
{
    int sum = 0;
    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] == boxChar)
            {
                sum += 100 * y + x;
            }
        }
    }

    return sum;
}

void ExecuteRobotMoves(char[][] grid)
{
    var (x, y) = Utils.FindLocationInGrid(grid, ROBOT);
    foreach (char c in robotMoves)
    {
        var (newX, newY) = Utils.AddPair(PositionChange(c), (x, y));

        char charAtNewSpot = grid[newY][newX];
        if (charAtNewSpot == EMPTY)
        {
            grid[y][x] = EMPTY;
            x = newX;
            y = newY;
            grid[y][x] = ROBOT;
        }
        else if (charAtNewSpot == BOX 
            || charAtNewSpot == BOX_LEFT 
            || charAtNewSpot == BOX_RIGHT)
        {
            int xVel = newX - x;
            int yVel = newY - y;

            int dx = xVel;
            int dy = yVel;

            var (freeX, freeY) = FindNextFreeSpaceInLine(grid, (newX, newY), (dx, dy));
            if (freeX == -1 || freeY == -1)
            {
                continue;
            }

            for (int tempX = freeX, tempY = freeY; 
                 newY != tempY || newX != tempX; 
                 tempY -= dy, tempX -= dx)
            {
                Utils.SwapInGrid(grid, (tempX, tempY), (tempX - dx, tempY - dy));
            }

            grid[y][x] = EMPTY;
            grid[newY][newX] = ROBOT;
            x = newX;
            y = newY;
        }
    }
}

(int, int) FindNextFreeSpaceInLine(char[][] grid, (int, int) startLocation, (int, int) diffs)
{
    var (x, y) = startLocation;
    var (dx, dy) = diffs;

    while (grid[y][x] != WALL)
    {
        if (grid[y][x] == EMPTY)
        {
            return (x, y);
        }

        x += dx;
        y += dy;
    }

    return (-1, -1);
}


string[] data = Utils.FileToString("day15").Split("\n\n");

var robotMoves = string.Join("", data[1].Split("\n"));
var warehouse = data[0].Split("\n")
    .Select(line => line.ToCharArray())
    .ToArray();

// var warehouse2 = warehouse.Select(line => 
//     line.Select(c => c switch 
//         {
//             EMPTY => new[] {'.', '.'},
//             BOX   => new[] {'[', ']'},
//             ROBOT => new[] {'@', '.'},
//             WALL  => new[] {'#', '#'},
//             _     => new[] { c }
//         })
//         .SelectMany(x => x)
//         .ToArray()
//     ).ToArray();


ExecuteRobotMoves(warehouse);
int part1 = CalculateGPSSum(warehouse, BOX);

// ExecuteRobotMoves(warehouse2);
// int part2 = CalculateGPSSum(warehouse2, BOX_LEFT);

Console.WriteLine($"Part 1: {part1}");
// Console.WriteLine($"Part 2: {part2}"); 

Utils.PrintGrid(warehouse2);
