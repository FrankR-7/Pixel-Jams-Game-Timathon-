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

print(map)

# just testing the first parts of the algo
room = Room(0,0,size[1],size[0], first=True)
map = room.split(map)

print(map)

