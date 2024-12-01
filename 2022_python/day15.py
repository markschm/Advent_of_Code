ROW_PART1 = 2000000
row_ranges, beacons_in_row = [], set()
min_x, max_x = 2**31, -2**31

with open("input/day15", "r") as f:
    for line in f.read().split("\n"):
        split_l = line.split()

        sensor = (int(split_l[2][2:-1]), int(split_l[3][2:-1]))
        beacon = (int(split_l[-2][2:-1]), int(split_l[-1][2:]))

        # if beacon is in the row for part 1
        if beacon[1] == ROW_PART1:
            beacons_in_row.add(beacon)

        distance = abs(sensor[0] - beacon[0]) + abs(sensor[1] - beacon[1])

        min_x = min(min_x, sensor[0] - distance)
        max_x = max(max_x, sensor[0] + distance)

        vertices = []
        for direction in [(1, 0), (-1, 0), (0, 1), (0, -1)]:
            vertices.append((sensor[0] + direction[0] * distance, sensor[1] + direction[1] * distance))
        
        # if covered range intersects with row
        if (vertices[2][1] >= ROW_PART1 and vertices[3][1] <= ROW_PART1):
            dis_to_row = abs(sensor[1] - ROW_PART1)
            dis_horizontal = distance - dis_to_row
            row_ranges.append((sensor[0] - dis_horizontal, sensor[0] + dis_horizontal + 1))
        
p1_row = [0] * (max_x - min_x)
for x_range in row_ranges:
    for x in range(x_range[0] - min_x, x_range[1] - min_x):
        p1_row[x] = 1

print(f"Part 1: {sum(p1_row) - len(beacons_in_row)}")