digits = {
    "one": "o1e", 
    "two": "t2o", 
    "three": "th3ee", 
    "four": "fo4r", 
    "five": "f5ve", 
    "six": "s6x", 
    "seven": "se7en", 
    "eight": "ei8ht", 
    "nine": "n9ne"
}

def computeLineValue(line):
    lineVal = ""
    for char in line:
        if char.isdigit():
            lineVal += char
            break

    for char in reversed(line):
        if char.isdigit():
            lineVal += char
            break

    return int(lineVal)
        

with open("input/day01", "r") as f:
    part1 = 0
    part2 = 0

    for line in f.read().split("\n"):
        # Part 1
        part1 += computeLineValue(line)

        # Part 2
        for word, wordWithDigit in digits.items():
            line = line.replace(word, wordWithDigit)
        part2 += computeLineValue(line)

    
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")
