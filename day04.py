with open("input/day04", "r") as f:
    part1 = 0
    part2 = 0

    fileSplitByLine = f.read().split("\n")
    copies = [1 for _ in range(len(fileSplitByLine))]

    for i, line in enumerate(fileSplitByLine):
        _, numbers = line.split(": ")
        
        winningNumbers, yourNumbers = map(str.split, numbers.split(" | "))

        wins = 0
        for num in yourNumbers:
            if num in winningNumbers:
                wins += 1

        part1 += pow(2, wins - 1) if wins > 0 else 0

        # Part 2
        for n in range(1, wins + 1):
            if i + n >= len(copies):
                break

            copies[i + n] += copies[i]


    print(f"Part 1: {part1}")
    print(f"Part 2: {sum(copies)}")
