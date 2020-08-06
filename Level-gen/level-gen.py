# import stuff
import sys
import numpy as np
import json
from room import Room

# set size of the map from cmd args
size = (int(sys.argv[2]), int(sys.argv[1]))

# create empty map and add bounding bprders
map = np.zeros(size, dtype='int')
map[0] = map[-1] = np.ones(size[1])
map = map.T
map[0] = map[-1] = np.ones(size[0])
map = map.T

# print(map)

# Recursive function to generate room boundaries
rooms = []
room = Room(0, 0, size[1], size[0])
rooms.append(room)


# Faulty
def generate_rooms(act, level):
    next = act.split(level)
    if next != -1:
        level = next[0]
        room = Room(*next[1:])
        rooms.append(room)
        if not room.state:
            level = generate_rooms(room, level)
    if not act.state:
        level = generate_rooms(act, level)
    return level


map = generate_rooms(room, map)

# Debugging and stuff, I dont like those negative rooms
print(map)
print('no. of rooms: ', len(rooms))
debug = list(zip([(room.width, room.height) for room in rooms], [(room.x1, room.x2) for room in rooms],
                 [(room.y1, room.y2) for room in rooms], [room.state for room in rooms]))
print(debug)
