f = open("input/day04", "r")
count1 = 0
count2 = 0

for line in f:
    elf1, elf2 = line.split(",")
    
    e1, e2 = elf1.split("-")
    e1_range = [int(e1), int(e2)]

    e3, e4 = elf2.split("-")
    e2_range = [int(e3), int(e4)]

    if (e1_range[0] <= e2_range[0]) and (e1_range[1] >= e2_range[1]):
        count1 += 1
    elif (e2_range[0] <= e1_range[0]) and (e2_range[1] >= e1_range[1]):
        count1 += 1

    if ((e1_range[0] >= e2_range[0]) and (e1_range[0] <= e2_range[1])) or ((e1_range[1] <= e2_range[1]) and (e1_range[1] >= e2_range[0])):
        count2 += 1
    elif ((e2_range[0] >= e1_range[0] and e2_range[0] <= e1_range[1]) or ((e2_range[1] <= e1_range[1]) and e2_range[1] >= e1_range[0])):
        count2 += 1 

print("Part 1: " + str(count1))
print("Part 2: " + str(count2))

