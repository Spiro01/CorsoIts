from Animale import *


class Leone(Animale):

    def __init__(self, name, age):
        super().__init__(name, age)
        self._peso = 150

    @property
    def Peso(self):
        return self._peso

    @Peso.setter
    def Peso(self, a):
        self._peso = int(a)

    def info(self):
        return self._peso

    def parla(self):
        return "Ruggisce"

    def muove(self):
        return "Va come un fulmine"

    def mangia(self):
        return "Divora"

    def beve(self):
        return "Ingurgita"
