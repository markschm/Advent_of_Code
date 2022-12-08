class Node:

    def __init__(self, parent, name):
        self.parent = parent
        self.name = name
        self.children = []
        self.files = []
        self.size = 0

    def getSize(self):

        if self.size == 0: # size hasn't yet been calculated
            for size in self.files: # add file sizes
                self.size += size

            for child in self.children: # add child directory sizes
                self.size += child.getSize()
        
        return self.size

    def addFile(self, file_size):
        self.files.append(file_size)

    def addChild(self, child_name):
        newNode = Node(self, child_name)
        self.children.append(newNode)

    def getParent(self):
        return self.parent

    def getChild(self, child_name):
        for child in self.children:
            if child.name == child_name:
                return child

        return None

##############################################
def findDirToRemove(target_space, node):
    smallest = TOTAL_DISK_SPACE

    for child in node.children:    
        smallest = min(findDirToRemove(target_space, child), smallest)

    if target_space <= node.getSize():
        return min(smallest, node.getSize())
    return smallest


def addValidSize(size): # return the dir size if it's in the allowed range
    return size if size <= MAX_SIZE else 0

##############################################
MAX_SIZE = 100000
TOTAL_DISK_SPACE = 70000000
MIN_UNUSED_SPACE = 30000000

f = open("input/day07", "r")

curr_node = Node(None, " ")
curr_node.addChild("/")

total_size = 0
for line in f:
    args = line.split()

    if args[0] == "$":
        if args[1] == "ls":
            continue

        if args[2] == "..":
            total_size += addValidSize(curr_node.getSize())
            curr_node = curr_node.getParent()

        else:
            curr_node = curr_node.getChild(args[2])
    
    else:
        if args[0].isdigit():
            curr_node.addFile(int(args[0]))

        else:
            curr_node.addChild(args[1])

# return to default directory
while curr_node.name != "/":
    total_size += addValidSize(curr_node.getSize())
    curr_node = curr_node.getParent()

space_to_free = MIN_UNUSED_SPACE - (TOTAL_DISK_SPACE - curr_node.getSize())

print("Part 1: " + str(total_size))
print("Part 2: " + str(findDirToRemove(space_to_free, curr_node)))