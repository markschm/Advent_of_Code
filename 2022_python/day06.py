
f = open("input/day06", "r").read()

packet_marker, message_marker = 4, 14

index = 0
while True:
    if packet_marker == 4 and len(set(f[index:index + 4])) == 4:
        packet_marker += index
    
    if len(set(f[index:index + 14])) == 14:
        message_marker += index
        break

    index += 1

print("Part 1: " + str(packet_marker))
print("Part 2: " + str(message_marker))