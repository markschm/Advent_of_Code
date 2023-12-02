class Head:
    def __init__(self):
        self.position = [0, 0]

    def move(self, direction):
        match direction:
            case "U":
                self.position[1] += 1
            case "D":
                self.position[1] -= 1
            case "R":
                self.position[0] += 1
            case "L":
                self.position[0] -= 1

################################################
class Tail:
    def __init__(self):
        self.position = [0, 0]
        self.visited = set()
        self.visited.add((0, 0))

    def addSpot(self, coordinate):
        self.visited.add(coordinate)

    def isTouching(self, head):
        if ((-1 <= head.position[0] - self.position[0] <= 1)
        and (-1 <= head.position[1] - self.position[1] <= 1)):
            return True
        return False

    def chaseHead(self, head):
        position = list(self.position)
        for i in range(2):
            movement = 0
            if head.position[i] != self.position[i]:
                movement = 1 if head.position[i] > self.position[i] else -1
            
            position[i] += movement

        self.position = position
        self.visited.add(tuple(self.position))

#################################################
data = open("input/day09", "r")
head = Head()
tails = [Tail() for _ in range(9)]

for line in data:
    args = line.split()

    for i in range(int(args[1])):
        head.move(args[0])

        if tails[0].isTouching(head) == False:
            tails[0].chaseHead(head)

            for i in range(1, 9):
                if tails[i].isTouching(tails[i - 1]):
                    break
                tails[i].chaseHead(tails[i - 1])

############################################################################
print(f"Part 1: {len(tails[0].visited)}")  # positions visited by first tail
print(f"Part 2: {len(tails[-1].visited)}") # positions visited by last tail