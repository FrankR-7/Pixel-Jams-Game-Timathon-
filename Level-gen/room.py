import random

class Room:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2
        self.start = (self.x1, self.y1)
        self.end = (self.x2, self.y2)
        self.width = (self.x2 - self.x1) - 2
        self.height = (self.y2 - self.y1) - 2
        self.size = self.width * self.height
        self.state = False
        if self.size <= 9:
            self.state = True
        elif (self.width in range(0, 7) and self.width > self.height) or (self.width in range(0, 7) and self.height > self.width):
            self.state = True
        elif (self.width <= 5) and (self.height <= 5):
            self.state = True
        elif (self.width < 0) or (self.height < 0) or (any([self.x1, self.x2, self.y1, self.y2]) < 0):
            self.state = True

    def _draw(self, axis, pos, matrix):
        pos = pos-1
        start = self.x1
        if axis == 'y':
            matrix = matrix.T
            start = self.y1

        for n, o in enumerate(matrix[pos, start:]):
            if n == 0 or n == len(matrix[pos, start:]):
                pass
            else:
                if o == 1:
                    break
                else:
                    matrix[pos, n] = 1

        if axis == 'y':
            matrix = matrix.T

        return matrix

    def split(self, matrix):
        if self.state:
            return -1
        if self.size <= 9:
            self.state = True
            return -1
        elif ((self.width in range(0, 7) and self.width > self.height) or (self.width in range(0, 7) and self.height > self.width)):
            self.state = True
            return -1
        elif (self.width <= 5) and (self.height <= 5):
            self.state = True
            return -1
        elif (self.width < 0) or (self.height < 0) or (any([self.x1, self.x2, self.y1, self.y2]) < 0):
            self.state = True
            return -1
        else:
            preMatrix = matrix.copy()
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
                    pt = random.randint(self.x1 + 3, self.x2 - 3)
                except:
                    self.state = True
                    return -1
            elif axis == 'y':
                try:
                    pt = random.randint(self.y1 + 3, self.y2 - 3)
                except:
                    self.state = True
                    return -1

            matrix = self._draw(axis, pt, matrix.copy())

            if axis == 'x':
                self.y2 = self.y2 - pt
            elif axis == 'y':
                self.x2 = self.x2 - pt

            if self.size <= 9:
                self.state = True
            elif (self.width == 5 and self.width > self.height) or (self.height == 5 and self.height > self.width):
                self.state = True

            if (self.width < 0) or (self.height < 0) or (any([self.x1, self.x2, self.y1, self.y2]) < 0):
                self.state = True
                return -1
            else:
                if axis == 'x':
                    nx, ny = self.x1, self.y2
                    ox, oy = self.x1, self.y2 + pt
                elif axis == 'y':
                    nx, ny = self.x2, self.y1
                    ox, oy = self.x2 + pt, self.y2

            return matrix, nx, ny, ox, oy
