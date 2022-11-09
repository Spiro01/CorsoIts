from Animale import *


class Cavallo(Animale):
    def __init__(self, name, age):
        super().__init__(name, age)
        self._mantello = "Colorato"

    @property
    def Mantello(self):
        return self._mantello

    @Mantello.setter
    def Mantello(self, a):
        self._mantello = a

    def info(self):
        return self._mantello

    def parla(self):
        return "Nitrisce"

    def muove(self):
        return "Galoppa"

    def mangia(self):
        return "Mangia"

    def beve(self):
        return "Beve"
