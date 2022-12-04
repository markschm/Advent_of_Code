def getPriority(c):
    return ord(c) - (96 if c.islower() else 38)

f = open("input/day03", "r").read().split()

#part 1
prioritySum1 = 0

for line in f:
    c1, c2 = sorted(line[:len(line) // 2]), sorted(line[len(line) // 2:])
    index1, index2 = 0, 0

    while index1 < len(c1) and index2 < len(c2):
        if (c1[index1] == c2[index2]):
            prioritySum1 += getPriority(c1[index1])
            break
        elif (c1[index1] < c2[index2]):
            index1 += 1
        else:
            index2 += 1

# part 2
prioritySum2 = 0
elfIndex = 0

while elfIndex < len(f):
    e1, e2, e3 = sorted(f[elfIndex]), sorted(f[elfIndex + 1]), sorted(f[elfIndex + 2])
    index1, index2, index3 = 0, 0, 0

    while index1 < len(e1) and index2 < len(e2) and index3 < len(e3):
        if (e1[index1] == e2[index2] and e2[index2] == e3[index3]):
            prioritySum2 += getPriority(e1[index1])
            break
        elif (e1[index1] < e2[index2]):
            index1 += 1
        elif (e3[index3] < e2[index2]):
            index3 += 1
        else:
            index2 += 1

    elfIndex += 3

print("Part 1: " + str(prioritySum1))
print("Part 2: " + str(prioritySum2))