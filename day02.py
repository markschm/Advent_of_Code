win = {
    'A': 2,
    'B': 3,
    'C': 1
}

lose = {
    'A': 3,
    'B': 1,
    'C': 2
}

f = open("input/day02", "r").read().split("\n")
score1, score2 = 0, 0


for line in f:
    elf, player = line.split()

    score1 += ord(player) - 87

    if elf == 'A' and player == 'Y':
        score1 += 6
    elif elf == 'B' and player == 'Z':
        score1 += 6
    elif elf == 'C' and player == 'X':
        score1 += 6
    elif ord(elf) - 65 == ord(player) - 88: # if moves are the saem
        score1 += 3

    if player == 'X': #lose
        score2 += lose[elf]
    elif player == 'Y': #tie
        score2 += 3 + ord(elf) - 64
    else: #win
        score2 += 6 + win[elf]


print("Part 1: " + str(score1))
print("Part 2: " + str(score2))