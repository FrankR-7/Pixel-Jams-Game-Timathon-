# import stuff
import sys
import numpy as np
import json
import random
from room import Room
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
    if not(act.state):
        generate_rooms(act)
    return None

generate_rooms(room)

# Room selection
normal_rooms = random.choices(rooms, k=25)

# Map generation (wip)
for room in rooms[::2]:
    map = room.draw(map.copy())

# Debugging and stuff
plt.xticks([])
plt.yticks([])
plt.imshow(map)
plt.show()

j = {'map': map.tolist()}
print(json.dumps(j))
