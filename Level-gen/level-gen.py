# import stuff
import sys
import numpy as np
import json
import random
from room import Room
from hall import Hall
import matplotlib.pyplot as plt

# ------- Getting everything ready -------

# set size of the map from cmd args
size = (int(sys.argv[2]), int(sys.argv[1]))
debug = sys.argv[3] if len(sys.argv) == 4 else None

# create empty map
map = np.zeros(size, dtype='int')

size = [unit - 1 for unit in size]

# Get the distribution of the items
items = ['upgrade', 'strength', 'heal', 'invisible', 'dew']

used = {i: 0 for i in items}

amt_items = np.mean(size) // 7

max_items = {i: amt_items + random.randrange(-2, 3) for i in items}

# -------- STAGE 1 ---------

rooms = []
room = Room(0, 0, size[1], size[0])
rooms.append(room)


# Recursive function to generate map skeleton
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

# -------- STAGE 2 and 3 (combined for performance) -----------

# Room selection
normal_rooms = rooms[::2].copy()
halls = []

# Select start, end and chest rooms
start = random.choice(normal_rooms)
start.type = 'start'

while True:
    end = random.choice(normal_rooms)
    if end.type == 'normal':
        end.type = 'end'
        break

if random.choices([True, False], [1, 24])[0]:  # 1 in 25 chance of getting a chest room
    while True:
        chest = random.choice(normal_rooms)
        if chest.type == 'normal':
            chest.type = 'chest'
            break
    while True:
        key = random.choice(normal_rooms)
        if key.type == 'normal':
            key.items.append('key')
            break

# Item distribution
for item in items:
    while max_items[item] > used[item]:
        act = random.choice(normal_rooms)
        if act.type == 'normal':
            act.items.append(item)
            used[item] += 1

# Getting halls ready
for room in rooms[1::2]:
    meta = (room.x1, room.y1, room.x2, room.y2)
    hall = Hall(*meta)
    map = hall.findmode(map.copy())
    halls.append(hall)

# Stage 2.5: Generating map

# Map generation
for room in normal_rooms:
    map = room.fill(map.copy())

for hall in halls:
    out = hall.generate(map.copy())
    if type(out) is not int:
        map = out
    else:
        map = hall.findmode(map.copy(), False)

for room in normal_rooms:
    map = room.draw_doors(map.copy())

for hall in halls:
    map = hall.generate_hall_1(map.copy())
for hall in halls:  # again
    if len(hall.meta):
        map = hall.generate_hall_2(map.copy())

# ------ Debugging ------
if debug == '1':
    plt.xticks([])
    plt.yticks([])
    plt.imshow(map)
    plt.colorbar(ticks=[i for i in range(15)])
    plt.show()
elif debug == '2':
    print(map)

# ------- Output --------
j = {'map': map.tolist()}
print(json.dumps(j))
