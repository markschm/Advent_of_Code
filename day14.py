# moves a unit of sand until it settles
def moveSandUnit():
    sand = [0, 150] # starting coordinate for sand

    while True:

        if cave[sand[0] + 1][sand[1]] in "#O":
            if " " in cave[sand[0] + 1][sand[1] - 1]:
                sand[1] -= 1
                continue
            elif " " in cave[sand[0] + 1][sand[1] + 1]:
                sand[1] += 1
                continue 
            else:
                cave[sand[0]][sand[1]] = "O"
                return
        
        # if sand flowed to the bottom
        if sand[0] == highest_y + 2:
            cave[sand[0]][sand[1]] = "O"
            return

        sand[0] += 1

# keep creating sand units until
# part 1: sand unit touches floor
# part 2: sand unit blocks the origin
def sandFlow():
    sand_units = 0
    parts = []

    while True:
        moveSandUnit()
        sand_units += 1

        for cell in cave[highest_y + 1]:
            if len(parts) == 0 and "O" in cell:
                parts.append(sand_units - 1)
            
            elif "O" in cave[0][150]:
                parts.append(sand_units)
                return parts

def printCave(cave):
    cave[0][150] = "+" # indicator for where sand flows from
    p_str = ""
    for i in range(len(cave)):
        p_str += ''.join(cave[i])
        p_str += "\n"

    print(p_str)

################################################################
cave = [[' '] * 500 for _ in range(170)]
highest_y = 0

# get the rock locations from the file
with open("input/day14", "r") as f:
    for line in f.read().split("\n"):
        positions = line.split(" -> ")

        for i in range(len(positions) - 1):
            x1, y1 = positions[i].split(",")
            x1, y1 = int(x1), int(y1)

            x2, y2 = positions[i + 1].split(",")
            x2, y2 = int(x2), int(y2)

            highest_y = max(highest_y, max(y1, y2))

            if x1 == x2: # if vertical rock line
                start, end = (y1, y2) if y1 < y2 else (y2, y1)
                for row in range(start, end + 1):
                    cave[row][x1 - 350] = "#"

            else: # if horizontal rock line
                start, end = (x1, x2) if x1 < x2 else (x2, x1)
                for col in range(start, end + 1):
                    cave[y1][col - 350] = "#"

# add the floor rocks
for i in range(len(cave[highest_y + 2])):
    cave[highest_y + 2][i] = "#"


part1, part2 = sandFlow()
# printCave(cave) # just used for testing but looks cool so i left it, zoom out super far in terminal to see it 

print(f"Part 1: {part1}")
print(f"Part 2: {part2}")