def buildAdjacentArray(row, numStart, numEnd, width, height):
    arr = [(numStart - 1, row - 1 + i) for i in range(3)] 
    arr.extend([(numEnd, row - 1 + i) for i in range(3)])
    arr.extend([(i, row - 1) for i in range(numStart, numEnd + 1)])
    arr.extend([(i, row + 1) for i in range(numStart, numEnd + 1)])
    
    i = 0
    while i < len(arr):
        if (arr[i][0] < 0 or arr[i][0] >= width
         or arr[i][1] < 0 or arr[i][1] >= height):
            arr.pop(i)
            continue

        i += 1

    return arr


def findDigit(x, y, grid):
    px1 = px2 = x

    while px1 - 1 >= 0 and grid[y][px1 - 1].isdigit():
        px1 -= 1

    while px2 < len(grid[0]) and grid[y][px2].isdigit():
        px2 += 1

    return int("".join(grid[y][px1:px2]))


with open("input/day03", "r") as f:

    grid = []
    for line in f.read().split("\n"):
        grid.append(list(line))

    width, height = len(grid[0]), len(grid)

    part1 = 0
    part2 = 0
    for row in range(height):
        for col in range(width):
            # Part 1
            if grid[row][col].isdigit() and (col == 0 or (col - 1 >= 0 and not grid[row][col - 1].isdigit())):
                numStart = col
                numEnd = col
                numVal = ""
                while numEnd < width and grid[row][numEnd].isdigit():
                    numVal += grid[row][numEnd]
                    numEnd += 1

                adjacentCells = buildAdjacentArray(row, numStart, numEnd, width, height)

                for x, y in adjacentCells:
                    if not grid[y][x].isdigit() and not grid[y][x] == ".":
                        part1 += int(numVal)
                        break


            # Part 2
            if grid[row][col] == "*":
                adjacentCells = buildAdjacentArray(row, col, col + 1, width, height)

                digits = []
                for x, y in adjacentCells:
                    if grid[y][x].isdigit():
                        partNum = findDigit(x, y, grid)

                        if partNum in digits:
                            continue

                        digits.append(partNum)

                if len(digits) == 2:
                    part2 += digits[0] * digits[1]
    
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")
