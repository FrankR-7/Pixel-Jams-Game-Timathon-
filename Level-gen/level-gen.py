# import stuff
import sys
import numpy as np
import json
from room import Room
import matplotlib.pyplot as plt

# set size of the map from cmd args
size = (int(sys.argv[2]), int(sys.argv[1]))

# create empty map and add bounding bprders
map = np.zeros(size, dtype='int')
map[0] = map[-1] = np.ones(size[1])
map = map.T
map[0] = map[-1] = np.ones(size[0])
map = map.T
map[0,0] = 2

size = [unit - 1 for unit in size]

# Recursive function to generate room boundaries
rooms = []
room = Room(0, 0, size[1], size[0])
rooms.append(room)

def generate_rooms(act, level):
    next = act.split(level)
    if next != -1:
        level = next[0]
        room = Room(*next[1:])
        rooms.append(room)
        if not room.state:
            level = generate_rooms(room, level)
    if not(act.state):
        level = generate_rooms(act, level)
    return level

map = generate_rooms(room, map)

# Don't worry about this v
'''
for room in rooms:
    map = room.generate_floor(map)
'''

# Debugging and stuff
plt.xticks([])
plt.yticks([])
plt.imshow(map)
plt.show()
print('no. of rooms: ', len(rooms))

print(json.dumps(map.tolist()))
