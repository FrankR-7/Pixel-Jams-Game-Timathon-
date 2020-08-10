import random

class Room:
    def __init__(self, x_1, y_1, x_2, y_2):
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2
        self.width = (self.x2 - self.x1)+1
        self.height = (self.y2 - self.y1)+1
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
        wallx = [i for i in range(self.x1, self.x2+1)]
        wally = [i for i in range(self.y1, self.y2+1)]

        for m in wallx:
            for n in [self.y1, self.y2]:
                matrix[n, m] = 1
        for m in wally:
            for n in [self.x1, self.x2]:
                matrix[m, n] = 1

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

            #I'll decide later if I wanna implemet this
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
        if self.type in ['hall','void_hall']:
            pass # Coming soon
        elif self.type in ['normal','chest','start','end']:
            matrix = self.draw_walls(matrix.copy())
            floorx = [i for i in range(self.x1+1, self.x2)]
            floory = [i for i in range(self.y1+1, self.y2)]
            for x in floorx:
                for y in floory:
                    if random.choice([True, False]):
                        matrix[y,x] = 2
                    else:
                        matrix[y,x] = 3

        return matrix
