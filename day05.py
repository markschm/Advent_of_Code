class RangeMap:
    def __init__(self, fp):
        # (lower, upper, source - destination)
        self.ranges = []

        self.populate(fp)

    def populate(self, fp):
        fp.readline() # read & ignore title

        line = f.readline()
        while line and line != "\n":
            destination, source, rangeLength = map(int, line.strip().split())

            self.ranges.append((source, source + rangeLength, destination - source))

            line = f.readline()

    def get(self, val):
        for lower, upper, diff in self.ranges:
            if lower <= val < upper:
                return val + diff
            
        return val


def computeLocation(seed, seedToSoil, soilToFert, fertToWater, 
                    waterToLight, lightToTemp, tempToHumid, humidToLocation):
    soil = seedToSoil.get(seed)
    fert = soilToFert.get(soil)
    water = fertToWater.get(fert)
    light = waterToLight.get(water)
    temp = lightToTemp.get(light)
    humid = tempToHumid.get(temp)
    
    return humidToLocation.get(humid)


with open("input/day05", "r") as f:
    part1 = 0
    part2 = 0

    _, seeds = map(str.split, f.readline().split(": "))
    f.readline() # empty line

    # Build all almanac maps
    seedToSoil = RangeMap(f)
    soilToFert = RangeMap(f)
    fertToWater = RangeMap(f)
    waterToLight = RangeMap(f)
    lightToTemp = RangeMap(f)
    tempToHumid = RangeMap(f)
    humidToLocation = RangeMap(f)

    print("successfully built maps")

    # Part 1
    location = 999999999
    for seed in map(int, seeds):
        location = min(
            location, 
            computeLocation(
                seed,
                seedToSoil, 
                soilToFert, 
                fertToWater, 
                waterToLight, 
                lightToTemp, 
                tempToHumid, 
                humidToLocation))

    part1 = location
    print(f"successful part 1 = {part1}")

    # Part 2
    location = 999999999
    i = 0
    while i < len(seeds):
        start, rangeLen = int(seeds[i]), int(seeds[i + 1])
        for n in range(start, start + rangeLen):
            location = min(
                location, 
                computeLocation(
                    n,
                    seedToSoil, 
                    soilToFert, 
                    fertToWater, 
                    waterToLight, 
                    lightToTemp, 
                    tempToHumid, 
                    humidToLocation))

        i += 2

    part2 = location
    print(f"successful part 2 = {part2}")


    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")
