from Models.Shape1D import Shape1D
import math


class Circle(Shape1D):
    def __init__(self, radius):
        super().__init__(radius)

    def Perimeter(self):
        return self.x * math.pi*2

    def Area(self):
        return math.pow(self.x, 2) * math.pi
