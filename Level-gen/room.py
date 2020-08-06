import random

class Room:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2
        self.start = (self.x1, self.y1)
        self.end = (self.x2, self.y2)
        self.width = (self.x2 - self.x1)
        self.height = (self.y2 - self.y1)
        self.size = self.width * self.height
        self.state = False
        if self.size <= 6:
            self.state = True
            print('1.1')
        elif (self.width < 4 and self.width > self.height) or (self.height < 4 and self.height > self.width):
            self.state = True
            print('2.1')
        elif (self.width < 4) and (self.height < 4):
            self.state = True
            print('3.1')
        elif (self.width <= 1) or (self.height <= 1):
            print('6.1')
        elif (self.x1 == self.x2) or (self.y1 == self.y2):
            self.state = True
            print('5.1')

    def _draw(self, matrix):
        wallx, wally = [], []
        for i in range(self.x1, self.x2):
            wallx.append(i)
        for i in range(self.y1, self.y2):
            wally.append(i)

        for m in wallx:
            for n in [self.y1, self.y2]:
                matrix[n, m] = 1
        for m in wally:
            for n in [self.x1, self.x2]:
                matrix[m, n] = 1

        return matrix

    def split(self, matrix):
        matrix = self._draw(matrix.copy())
        if self.state:
            print('0')
            return -1
        elif self.size <= 6:
            self.state = True
            print('1.2')
            return -1
        elif ((self.width < 4 and self.width > self.height) or (self.height < 4 and self.height > self.width)):
            self.state = True
            print('2.2')
            return -1
        elif (self.width < 4) and (self.height < 4):
            self.state = True
            print('3.2')
            return -1
        elif (self.width <= 1) or (self.height <= 1):
            self.state = True
            print('6.2')
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
                    print('8.1')
                    self.state = True
                    return -1
            elif axis == 'y':
                try:
                    pt = random.randint(self.x1 + 3, self.x2 - 3)
                except:
                    print('8.2')
                    self.state = True
                    return -1

            matrix = self._draw(matrix.copy())

            if axis == 'x':
                oy = self.y2
                self.y2 = (self.y2 - pt) + self.y1
            elif axis == 'y':
                ox = self.x2
                self.x2 = (self.x2 - pt) + self.x1

            self.width = (self.x2 - self.x1) - 2
            self.height = (self.y2 - self.y1) - 2
            self.size = self.width * self.height

            '''if (self.width <= 1) or (self.height <= 1):
                self.state = True
                print('6.3')
                return -1
            else:'''
            if axis == 'x':
                nx, ny = self.x1, self.y2
                ox = self.x1
            elif axis == 'y':
                nx, ny = self.x2, self.y1
                oy =  self.y2

            return matrix, nx, ny, ox, oy
