import copy

####################################################################
def computeOperation(operation, worry_level):
    x1 = worry_level if "old" in operation[0] else int(operation[0])
    x2 = worry_level if "old" in operation[2] else int(operation[2])

    match operation[1]:
        case '*':
            return x1 * x2
        case '/':
            return x1 / x2
        case '+':
            return x1 + x2
        case '-':
            return x1 - x2

def monkeysPlay(monkeys, rounds, worry_manager=0):
    for i in range(rounds):
        for monkey in monkeys:
            while len(monkey['items']) > 0:
                item = computeOperation(monkey['operation'], monkey['items'].pop(0))

                if worry_manager == 0: 
                    item //= 3
                else: 
                    item %= worry_manager

                if item % monkey['test_val'] == 0: 
                    monkeys[monkey['throw_to'][0]]['items'].append(item)
                else: 
                    monkeys[monkey['throw_to'][1]]['items'].append(item)

                monkey['inspections'] += 1

def maxInspectionsProduct(monkeys):
    max_inspections = [0, 0]
    for m in monkeys:
        max_inspections.append(m['inspections'])
        max_inspections.sort()
        max_inspections.pop(0)

    return max_inspections[0] * max_inspections[1]

####################################################################
data = open("input/day11", "r").read().split("\n\n")
monkeys1, worry_manager = [], 1

# get monkey data
for i in range(len(data)):
    lines = data[i].split("\n")

    monkey = dict()
    monkey['items'] = [int(num) for num in lines[1][18:].split(",")]
    monkey['operation'] = lines[2][19:].split()
    monkey['test_val'] = int(lines[3].split()[-1])
    monkey['throw_to'] = (int(lines[4].split()[-1]), int(lines[5].split()[-1]))
    monkey['inspections'] = 0
    monkeys1.append(monkey)

    worry_manager *= monkey['test_val']

monkeys2 = copy.deepcopy(monkeys1) # make copy for part 2


monkeysPlay(monkeys1, 20)
print(f"Part 1: {maxInspectionsProduct(monkeys1)}")

monkeysPlay(monkeys2, 10000, worry_manager)
print(f"Part 2: {maxInspectionsProduct(monkeys2)}")