from Animale import *
from Cane import *
from Cavallo import *
from Leone import *
import random
import time

NomiDisponibili = [
    "William",
    "Kimberly",
    "Maria",
    "Robert",
    "Michael",
    "Michelle",
    "Richard",
    "David",
    "John",
    "Dorothy",
    "Helen",
    "Elizabeth",
    "Carol",
    "James",
    "Nancy"
]

listLength = len(NomiDisponibili) - 1

while (1):

    listaAnimali = []

    for i in range(random.randint(1, 10)):
        listaAnimali.append(
            Cane(NomiDisponibili[random.randint(0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animale 10 cani

    for i in range(random.randint(1, 10)):
        listaAnimali.append(Cavallo(NomiDisponibili[random.randint(
            0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animale 10 cavalli

    for i in range(random.randint(1, 10)):
        listaAnimali.append(Leone(NomiDisponibili[random.randint(
            0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animale 10 cani

    for i in range(0, 20):
        animale = listaAnimali[random.randint(0, 9)]
        print(
            f"Nome = {animale.Name}; Eta = {animale.Age}; Info = {animale.info()}")
        metodi = dir(animale)
        metodoChiamato = metodi[random.randint(0, len(metodi)-1)]
        metodo = getattr(animale, metodoChiamato)
        if (metodo.__name__ != "dorme"):
            metodo()
        else:
            metodo(2)
    time.sleep(1)
