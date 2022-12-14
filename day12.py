# get height of char value
def getHeight(char):
    if char == "S":
        return 0
    elif char == "E":
        return 25
    else:
        return ord(char) - 97

# get neighbour grid coordinates
def getNeighbours(pos, grid):
    neighbours = []

    for r, c in [(1, 0), (-1, 0), (0, 1), (0, -1)]:
        r += pos[0]
        c += pos[1]

        if (
            (0 <= c < WIDTH) 
            and (0 <= r < HEIGHT) 
            and (getHeight(grid[pos[0]][pos[1]]) + 1 >= getHeight(grid[r][c]))
        ):
            neighbours.append((r, c))

    return neighbours

# breadth first search to get path length from a coordinate
def getPathLength(start_coord, grid):
    visited = [[False] * WIDTH for _ in range(HEIGHT)]
    queue = [(start_coord[0], start_coord[1], 0)]

    while queue:
        pos = queue.pop(0)

        if visited[pos[0]][pos[1]] == True:
            continue

        if grid[pos[0]][pos[1]] == "E":
            return pos[2] # steps taken to get to end

        visited[pos[0]][pos[1]] = True
        for row, col in getNeighbours(pos, grid):
            queue.append((row, col, pos[2] + 1))

    return 2**31 # if the end grid cell wasn't found

#########################################################################
data = open("input/day12", "r").read().strip().split()
grid = [[char for char in line] for line in data]

HEIGHT = len(grid)
WIDTH = len(grid[0])

part2 = 2**31
# get coordinates for start
for row in range(HEIGHT):
    for col in range(WIDTH):
        if grid[row][col] == "S":
            part1 = getPathLength((row, col), grid)

        if getHeight(grid[row][col]) == 0:
            part2 = min(getPathLength((row, col), grid), part2)

print(f"Part 1: {part1}")
print(f"Part 2: {part2}")