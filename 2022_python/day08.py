trees = open("input/day08", "r").read().split()

WIDTH = len(trees[0])
HEIGHT = len(trees)

tree_view = [[0 for _ in range(WIDTH)] for _ in range(HEIGHT)]

for row in range(HEIGHT):
    tree_view[row][0], tree_view[row][-1] = 1, 1 # set edge values to 1
    front_index, back_index = 0, WIDTH - 1

    for i in range(WIDTH):
        if trees[row][front_index] < trees[row][i]:
            tree_view[row][i] = 1
            front_index = i

        if trees[row][back_index] < trees[row][WIDTH - i - 1]:
            tree_view[row][WIDTH - i - 1] = 1
            back_index = WIDTH - i - 1

for col in range(WIDTH):
    tree_view[0][col], tree_view[-1][col] = 1, 1 # set edge values to 1
    front_index, back_index = 0, HEIGHT - 1

    for i in range(HEIGHT):
        if trees[front_index][col] < trees[i][col]:
            tree_view[i][col] = 1
            front_index = i

        if trees[back_index][col] < trees[HEIGHT - i - 1][col]:
            tree_view[HEIGHT - i - 1][col] = 1
            back_index = HEIGHT - i - 1

total_sum = 0
for row in range(HEIGHT):
    total_sum += sum(tree_view[row])

print(f"Part 1: {total_sum}")


# get the view number of a direction
def getView(tree_list, start_height):
    view = 0

    for tree in tree_list:
        view += 1
        if tree >= start_height:
            break
    return view

scenic_score = 0
for row in range(1, HEIGHT - 1):
    for col in range(1, WIDTH - 1):

        left_trees = trees[row][:col][::-1]
        right_trees = trees[row][col + 1:]
        up_trees = [trees[i][col] for i in range(row - 1, -1, -1)]
        down_trees = [trees[i][col] for i in range(row + 1, HEIGHT)]

        t_height = trees[row][col]

        view_score = (getView(left_trees, t_height) 
                    * getView(right_trees, t_height) 
                    * getView(up_trees, t_height)
                    * getView(down_trees, t_height))

        scenic_score = max(scenic_score, view_score)

print(f"Part 2: {scenic_score}")