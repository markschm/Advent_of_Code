data = open("input/day10", "r")

cycles, x, signal_strength = 0, 1, 0
command_cycles = 0

def printSymbol(cycles, x):
    new_line = "\n" if cycles % 40 == 0 else ""

    if 1 >= abs(cycles % 40 - x):
        return new_line + "#"
    return new_line + "."

part2 = ""
for line in data:
    command_cycles = 1 if "noop" in line else 2

    for _ in range(command_cycles):
        part2 += printSymbol(cycles, x)
        cycles += 1

        if cycles % 40 == 20:
            signal_strength += cycles * x

    if command_cycles == 2:
        x += int(line.split()[1])

print(f"Part 1: {signal_strength}")
print(f"Part 2: {part2}")