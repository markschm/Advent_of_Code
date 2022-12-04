def addToList(max_elfs, elf):
    max_elfs.append(elf)
    max_elfs.sort()
    del max_elfs[0]


with open("input/day01") as f:

    max_elfs, elf = [0, 0, 0], 0
    line = f.readline()

    while line:
        if line == "\n":
            if elf > max_elfs[0]:
                addToList(max_elfs, elf)

            elf = 0
        
        else:
            elf += int(line)

        line = f.readline()
    
    addToList(max_elfs, elf)


print("Part 1: " + str(max_elfs[-1]))
print("Part 2: " + str(sum(max_elfs)))