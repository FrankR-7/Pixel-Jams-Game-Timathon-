import random

class Room:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2
        self.start = (self.x1, self.y1)
        self.end = (self.x2, self.y2)
        self.width = (self.x2 - self.x1)+1
        self.height = (self.y2 - self.y1)+1
        self.size = self.width * self.height
        self.state = False
        if self.size < 9:
            self.state = True
        elif (self.width <= 8) and (self.height <= 8):
            self.state = True
        elif (self.x1 == self.x2) or (self.y1 == self.y2):
            self.state = True

    def _draw(self, matrix):
        wallx = [i for i in range(self.x1, self.x2)]
        wally = [i for i in range(self.y1, self.y2)]

        for m in wallx:
            for n in [self.y1, self.y2]:
                matrix[n, m] = 1
        for m in wally:
            for n in [self.x1, self.x2]:
                matrix[m, n] = 1

        return matrix

    def split(self, matrix):
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

            if axis == 'x':
                oy = self.y2
                self.y2 = pt # (self.y2 - pt) + self.y1
            elif axis == 'y':
                ox = self.x2
                self.x2 = pt # (self.x2 - pt) + self.x1

            self.width = (self.x2 - self.x1)
            self.height = (self.y2 - self.y1)
            self.size = self.width * self.height

            matrix = self._draw(matrix.copy())

            if axis == 'x':
                nx, ny = self.x1, self.y2
                ox = self.x2
            elif axis == 'y':
                nx, ny = self.x2, self.y1
                oy = self.y2

            return matrix, nx, ny, ox, oy

    def generate_floor(self, matrix):
        sx1, sx2, sy1, sy2, floor = self.x1+1, self.x2-1, self.y1+1, self.y2-1, []
        for x in range(sx1, sx2):
            for y in range(sy1, sx2):
                #floor.append((x, y))
                matrix[y, x] = 2

        #for n, m in floor:
            #matrix[n, m] = 2

        return matrix
