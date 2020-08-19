import random
from pathfinding.core.grid import Grid
from pathfinding.core.diagonal_movement import DiagonalMovement
from pathfinding.core.heuristic import manhatten
from pathfinding.finder.best_first import BestFirst
import numpy as np


class Hall:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.y1 = x_1, y_1
        self.x2, self.y2 = x_2, y_2
        self.width = (self.x2 - self.x1) + 1
        self.height = (self.y2 - self.y1) + 1
        self.doors = self.meta = []
        self.type = 'normal' if random.choices([True, False], [12, 1])[0] else 'void'

    def findmode(self, matrix, b=True):
        sx1, sy1 = self.x1 + 1, self.y1 + 1
        sx2, sy2 = self.x2 - 1, self.y2 - 1
        self.fx = [i for i in range(sx1, self.x2)]
        self.fy = [i for i in range(sy1, self.y2)]

        for m in self.fx:
            for n in [sy1, sy2]:
                matrix[n, m] = 2 if b else 0
        for m in self.fy:
            for n in [sx1, sx2]:
                matrix[m, n] = 2 if b else 0

        return matrix

    def generate_hall_1(self, matrix):
        doors = self.doors

        if len(doors) == 2:
            door1, door2 = doors

            mini = np.negative(matrix.copy()).tolist()
            for s, y in enumerate(mini):
                for t, x in enumerate(y):
                    if mini[s][t] == 0 or mini[s][t] == -4:
                        mini[s][t] = 1

            grid = Grid(matrix=mini)

            finder = BestFirst(diagonal_movement=DiagonalMovement.never, heuristic=manhatten)

            start = grid.node(door1[0], door1[1])
            end = grid.node(door2[0], door2[1])

            pos, _ = finder.find_path(start, end, grid)

            for p in pos:
                if (p[1] == doors[1][1]) and (p[0] == doors[1][0]):
                    matrix[p[1], p[0]] = 4
                elif (p[1] == doors[0][1]) and (p[0] == doors[0][0]):
                    matrix[p[1], p[0]] = 4
                else:
                    matrix[p[1], p[0]] = 2

            self.meta = doors + pos

        return matrix

    def generate_hall_2(self, matrix):
        def addwalls(y, x):
            a = x - 1
            b = x + 1
            c = y - 1
            d = y + 1
            try:
                for x in [a, b]:
                    for y in [c, d]:
                        if matrix[y, x] == 0:
                            matrix[y, x] = 1
            except:
                pass

        if self.type == 'normal':
            for l in self.meta:
                addwalls(l[1], l[0])

        return matrix

    def find_neighbors(self, matrix):
        up, down, left, right, doors = [], [], [], [], []
        for n, o, q in zip(*[[self.y1 - 1, self.y2 + 1], [self.y1, self.y2], [1, 2]]):
            for m in self.fx:
                try:
                    if matrix[o, m] == 4:
                        doors.append((m, o, q))
                    elif matrix[n, m] != 0 and matrix[n, m] != 1 and matrix[n, m] != 4:
                        if q == 1:
                            up.append((m, o, q))
                        else:
                            down.append((m, o, q))
                except:
                    pass
        for n, o, q in zip(*[[self.x1 - 1, self.x2 + 1], [self.x1, self.x2], [3, 4]]):
            for m in self.fy:
                try:
                    if matrix[m, o] == 4:
                        doors.append((o, m, q))
                    elif matrix[m, n] != 0 and matrix[m, n] != 1 and matrix[n, m] != 4:
                        if q == 3:
                            left.append((o, m, q))
                        else:
                            right.append((o, m, q))
                except:
                    pass

        up = random.choice(up) if len(up) >= 1 else -1
        down = random.choice(down) if len(down) >= 1 else -1
        left = random.choice(left) if len(left) >= 1 else -1
        right = random.choice(right) if len(right) >= 1 else -1

        return up, down, left, right, doors

    def generate(self, matrix):
        # Remove find mode
        matrix = self.findmode(matrix, False)

        amt = 2
        *sides, doors = self.find_neighbors(matrix)
        amt -= len(doors)
        if not amt <= 0:
            for door in doors:
                l = door[2]
                sides[l - 1] = -1

        sides = [side for side in sides if side != -1]
        while amt != 0 and len(sides) != 0:
            w = random.choice(sides)
            doors.append(w)
            sides.remove(w)
            amt -= 1
            if amt == 0 or len(sides) == 0:
                break

        if len(doors) <= 1:
            return -1

        for door in doors:
            matrix[door[1], door[0]] = 4

        self.doors = doors

        return matrix
