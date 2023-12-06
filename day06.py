def getResFromPairs(pairs):
    res = 1

    for time, distance in pairs:
        halfTime = time // 2

        i = 0
        while True:
            speed = halfTime - i
            raceTime = time - speed

            if speed < 0 or speed * raceTime <= distance:
                break            

            i += 1

        res *= (i * 2) + (0 if time % 2 == 1 else -1)

    return res


with open("input/day06", "r") as f:
    _, timeArr = map(str.split, map(str.strip, f.readline().split(":")))
    _, distanceArr = map(str.split, map(str.strip, f.readline().split(":")))

    # Part 1
    pairs = [(int(timeArr[i]), int(distanceArr[i])) for i in range(len(timeArr))]
    part1 = getResFromPairs(pairs)
    
    # Part 2
    singlePair = [(int("".join(timeArr)), int("".join(distanceArr)))]
    part2 = getResFromPairs(singlePair)

    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")
