from Animale import *


class Cane(Animale):
    def __init__(self, name, age):
        super().__init__(name, age)
        self._razza = "Pastore tedesco"

    @property
    def Razza(self):
        return self._razza

    @Razza.setter
    def Razza(self, a):
        self._razza = a

    def info(self):
        return self._razza

    def parla(self):
        return "Abbaia"

    def muove(self):
        return "Corre"

    def mangia(self):
        return "Mangia"

    def beve(self):
        return "Beve"
