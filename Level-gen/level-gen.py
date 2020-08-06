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

levels = []

# Faulty
def generate_rooms(act, level):
    next = act.split(level)
    # print(next if type(next)==int else '')
    print(act.size)
    if next != -1:
        level = next[0]
        levels.append(level)
        room = Room(*next[1:])
        rooms.append(room)
        if not room.state:
            level = generate_rooms(room, level)
    if not(act.state):
        level = generate_rooms(act, level)
    return level


map = generate_rooms(room, map)

# Debugging and stuff, I dont like those negative rooms
plt.xticks([])
plt.yticks([])
plt.imshow(map, cmap='Greys')
# plt.scatter([room.x1 for room in rooms] + [room.x2 for room in rooms], [room.y1 for room in rooms] + [room.y2 for room in rooms])
plt.show()
print('no. of rooms: ', len(rooms))
# debug = list(zip([(room.width, room.height) for room in rooms], [room.start for room in rooms],
                 #[room.end for room in rooms], [room.size for room in rooms]))
# print(debug)
# print(levels)
