import time


class Animale:

    def __init__(self, name, age):
        self._name = name
        self._age = age

    def __dir__(self):
        return ["info","parla","mangia","beve","dorme"]
    def info():
        pass

    def parla():
        pass

    def muove():
        pass

    def mangia():
        pass

    def beve():
        pass

    def dorme(self, durata):
        time.sleep(durata)

# ----Nome----
    @property
    def Name(self):
        return self._name

    @Name.setter
    def Name(self, a):
        self._name = a

# ----Eta----
    @property
    def Age(self):
        return self._age

    @Age.setter
    def Age(self, a):
        self._age = a
