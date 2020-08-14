# import stuff
import sys
import numpy as np
import json
import random
from room import Room
from hall import Hall
import matplotlib.pyplot as plt

# set size of the map from cmd args
size = (int(sys.argv[2]), int(sys.argv[1]))

# create empty map and add bounding bprders
map = np.zeros(size, dtype='int')

size = [unit - 1 for unit in size]

# Recursive function to generate room boundaries
rooms = []
room = Room(0, 0, size[1], size[0])
rooms.append(room)


def generate_rooms(act):
    next = act.split()
    if next != -1:
        room = Room(*next)
        rooms.append(room)
        if not room.state:
            generate_rooms(room)
    if not (act.state):
        generate_rooms(act)
    return None


generate_rooms(room)

# Room selection
normal_rooms = rooms[::2].copy()
halls = []

# Hallway system
for room in rooms[1::2]:
    meta = (room.x1, room.y1, room.x2, room.y2)
    hall = Hall(*meta)
    map = hall.findmode(map.copy())
    halls.append(hall)

# Map generation (wip)
for room in normal_rooms:
    map = room.draw(map.copy())

for hall in halls:
    out = hall.generate(map.copy())
    if type(out) is not int:
        map = out
    else:
        map = hall.findmode(map.copy(),False)

for room in normal_rooms:
    map = room.draw_doors(map.copy())

for hall in halls:
    map = hall.generate_hall_1(map.copy())
for hall in halls: # again
    if len(hall.meta):
        map = hall.generate_hall_2(map.copy())

# Debugging and stuff
plt.xticks([])
plt.yticks([])
plt.imshow(map)
plt.show()

j = {'map': map.tolist()}
print(json.dumps(j))
