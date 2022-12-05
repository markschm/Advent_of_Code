NUMBER_OF_STACKS = 9

f = open("input/day05", "r")
stacks1 = [[] for i in range(NUMBER_OF_STACKS)]

# setup starting stacks
for line in f:
    if line[1].isdigit():
        line = f.readline() # remove white space line
        break

    for i in range(9):
        if line[i * 4 + 1].isalpha():
            stacks1[i].append(line[i * 4 + 1])

# reverse lists so the top crate is at the top of the stack
for i in range(NUMBER_OF_STACKS):
    stacks1[i] = stacks1[i][::-1]

# copy starting stack for part 2
stacks2 = [[stacks1[i][n] for n in range(len(stacks1[i]))] for i in range(NUMBER_OF_STACKS)]

# read crate movements
for line in f:
    splitLine = line.strip().split(" ")
    num_to_move, source, destination = int(splitLine[1]), int(splitLine[3]) - 1, int(splitLine[5]) - 1
    
    for i in range(num_to_move):
        stacks2[destination].append(stacks2[source][-(num_to_move - i)])
        
    for _ in range(num_to_move):
        stacks1[destination].append(stacks1[source].pop())
        stacks2[source].pop()

# output strings
top_crates1, top_crates2 = "", ""
for i in range(NUMBER_OF_STACKS):
    top_crates1 += stacks1[i][-1]
    top_crates2 += stacks2[i][-1]

print("Part 1: " + top_crates1)
print("Part 2: " + top_crates2)