import random

class Room:
    def __init__(self, x_1, y_1, x_2, y_2, neighbors=None, first=False):
        self.start = (x_1, y_1)
        self.end = (x_2, y_2)
        if first:
            self.neighbors = None
        else:
            self.neighbors = neighbors
        self.width = (x_2 - x_1) - 2
        self.height = (y_2 - y_1) - 2
        self.size = self.width * self.height
        self.x1, self.x2, self.y1, self.y2 = x_1, x_2, y_1, y_2

    def _draw(self, axis, pos, matrix):
        if axis == 'y':
            matrix = matrix.T

        for n, o in enumerate(matrix[pos]):
            if n == 0 or n == len(matrix[pos]):
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
        # determine which direction the split will be
        _axis = 'x'
        if self.width > self.height:
            _axis = 'y'
        elif self.height > self.width:
            _axis = 'x'
        elif self.width == self.height:
            _axis = random.choice(('x', 'y'))

        # determine where the split will be
        if _axis == 'x':
            _pt = random.randint(self.x1 + 3, self.x2 - 3)
        elif _axis == 'y':
            _pt = random.randint(self.y1 + 3, self.y2 - 3)

        matrix = self._draw(_axis, _pt, matrix.copy())

        return matrix
