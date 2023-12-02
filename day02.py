loaded = {
    "red": 12,
    "green": 13,
    "blue": 14
}


def isGameValid(gameHands):
    for handful in gameHands:
        for pair in handful.split(", "):
            amount, color = pair.split()

            if int(amount) > loaded[color]:
                return False
            
    return True


def computeGamePower(gameHands):
    minValues = {
        "red": 0,
        "green": 0,
        "blue": 0
    }
    
    for handful in gameHands:
        for pair in handful.split(", "):
            amount, color = pair.split()

            minValues[color] = max(minValues[color], int(amount))

    return minValues["red"] * minValues["green"] * minValues["blue"]


with open("input/day02", "r") as f:
    part1 = 0
    part2 = 0

    for line in f.read().split("\n"):
        splitLine = line.split()

        gameHands = " ".join(splitLine[2:]).split(";")
        
        # Part 1
        if isGameValid(gameHands):
            part1 += int(splitLine[1][:-1])

        # Part 2
        part2 += computeGamePower(gameHands)

    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")
