import random


class Room:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2
        self.width = (self.x2 - self.x1) + 1
        self.height = (self.y2 - self.y1) + 1
        self.size = self.width * self.height
        self.type = 'normal'
        self.state = False
        if self.size < 9:
            self.state = True
        elif (self.width <= 8) and (self.height <= 8):
            self.state = True
        elif (self.x1 == self.x2) or (self.y1 == self.y2):
            self.state = True

    def draw_walls(self, matrix):
        # This works, but I think I can optimise this better
        # If performance ever becomes an issue, I'll change this part
        wallx = [i for i in range(self.x1, self.x2 + 1)]
        wally = [i for i in range(self.y1, self.y2 + 1)]

        for m in wallx:
            for n in [self.y1, self.y2]:
                matrix[n, m] = 1
        for m in wally:
            for n in [self.x1, self.x2]:
                matrix[m, n] = 1

        return matrix

    def find_neighbors(self, matrix):
        sx1, sy1 = self.x1 + 1, self.y1 + 1
        fx = [i for i in range(sx1, self.x2)]
        fy = [i for i in range(sy1, self.y2)]
        up, down, left, right, doors = [], [], [], [], []
        for n, o, q in zip(*[[self.y1 - 1, self.y2 + 1], [self.y1, self.y2], [1, 2]]):
            for m in fx:
                try:
                    if matrix[o, m] == 4:
                        doors.append((m, o, q))
                        break
                    elif matrix[n, m] != 0 and matrix[n, m] != 1 and matrix[n, m] != 4:
                        if q == 1:
                            up.append((m, o))
                        else:
                            down.append((m, o))
                except:
                    pass
        for n, o, q in zip(*[[self.x1 - 1, self.x2 + 1], [self.x1, self.x2], [3, 4]]):
            for m in fy:
                try:
                    if matrix[m, o] == 4:
                        doors.append((o, m, q))
                        break
                    elif matrix[m, n] != 0 and matrix[m, n] != 1 and matrix[n, m] != 4:
                        if q == 3:
                            left.append((o, m))
                        else:
                            right.append((o, m))
                except:
                    pass

        up = random.choice(up) if len(up) >= 1 else -1
        down = random.choice(down) if len(down) >= 1 else -1
        left = random.choice(left) if len(left) >= 1 else -1
        right = random.choice(right) if len(right) >= 1 else -1

        return up, down, left, right, doors

    def draw_doors(self, matrix):
        amt = random.choices([2,3],[2,1])[0]

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

        if len(doors) >= 1:
            for door in doors:
                matrix[door[1], door[0]] = 4

        return matrix

    def split(self):
        # Check for any end cases
        if self.state:
            return -1
        elif self.size < 9:
            self.state = True
            return -1
        elif (self.width <= 8) and (self.height <= 8):
            self.state = True
            return -1
        else:
            # determine which direction the split will be
            axis = 'x'
            if self.width > self.height:
                axis = 'y'
            elif self.height > self.width:
                axis = 'x'
            elif self.width == self.height:
                axis = random.choice(('x', 'y'))

            # determine where the split will be
            if axis == 'x':
                try:
                    pt = random.randint(self.y1 + 3, self.y2 - 3)
                except:
                    self.state = True
                    return -1
            elif axis == 'y':
                try:
                    pt = random.randint(self.x1 + 3, self.x2 - 3)
                except:
                    self.state = True
                    return -1

            # Update variables
            if axis == 'x':
                oy = self.y2
                self.y2 = pt
            elif axis == 'y':
                ox = self.x2
                self.x2 = pt

            self.width = (self.x2 - self.x1)
            self.height = (self.y2 - self.y1)
            self.size = self.width * self.height

            # I'll decide later if I wanna implemet this
            '''if self.size < 9:
                self.state = True
                return -1'''

            # Prepare variables for the child room
            if axis == 'x':
                nx, ny = self.x1, self.y2
                ox = self.x2
            elif axis == 'y':
                nx, ny = self.x2, self.y1
                oy = self.y2

            return nx, ny, ox, oy

    def draw(self, matrix):
        # Unified func for updating the map, will give better performance
        matrix = self.draw_walls(matrix.copy())
        floorx = [i for i in range(self.x1 + 1, self.x2)]
        floory = [i for i in range(self.y1 + 1, self.y2)]
        for x in floorx:
            for y in floory:
                if random.choices([True, False], [3, 1])[0]:
                    matrix[y, x] = 2
                else:
                    matrix[y, x] = 3

        return matrix
