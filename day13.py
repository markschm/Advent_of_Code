def compareInts(x, y):
    if x < y:
        return -1
    elif x == y:
        return 0
    else:
        return 1

# compare packets
def compare(p1, p2):
    if isinstance(p1, int) and isinstance(p2, int):
        return compareInts(p1, p2)

    elif isinstance(p1, int) and isinstance(p2, list):
        return compare([p1], p2)

    elif isinstance(p1, list) and isinstance(p2, int):
        return compare(p1, [p2])
    
    elif isinstance(p1, list) and isinstance(p2, list):
        for i in range(max(len(p1), len(p2))):
            if i >= len(p2):
                return 1
            if i >= len(p1):
                return -1
            
            res = compare(p1[i], p2[i])
            if res == -1:
                return -1
            elif res == 1:
                return 1
    return 0 # if lists were identical

########################################################
from functools import cmp_to_key

with open("input/day13", "r") as f:
    pairs = f.read().strip().split("\n\n")

packets, part1 = [], 0
for pair in pairs:
    p1, p2 = pair.split()
    p1, p2 = eval(p1), eval(p2)

    packets.append(p1)
    packets.append(p2)

    if compare(p1, p2) != 1:
        part1 += len(packets) // 2

# add divider packets
packets.append([[6]])
packets.append([[2]])

packets = sorted(packets, key=cmp_to_key(compare))
part2 = (packets.index([[2]]) + 1) * (packets.index([[6]]) + 1)

print(f"Part 1: {part1}")
print(f"Part 2: {part2}")