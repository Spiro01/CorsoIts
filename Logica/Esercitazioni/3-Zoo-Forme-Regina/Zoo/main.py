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
            Cane(NomiDisponibili[random.randint(0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animali 10 cani

    for i in range(random.randint(1, 10)):
        listaAnimali.append(Cavallo(NomiDisponibili[random.randint(
            0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animali 10 cavalli

    for i in range(random.randint(1, 10)):
        listaAnimali.append(Leone(NomiDisponibili[random.randint(
            0, listLength)], random.randint(1, 15)))  # Aggiungo alla lista degli animali 10 cani

    for i in range(0, 20):
        
        #Estraggo un animale casuale dai 30 generati
        animale = listaAnimali[random.randint(0, 9)]
        
        print(f"Nome = {animale.Name}; Eta = {animale.Age}; Info = {animale.info()}")
        
        #Cerco tutti i metodi contenuti nell'oggetto animale e ne estraggo uno random
        metodi = dir(animale)
        metodoChiamato = metodi[random.randint(0, len(metodi)-1)]
        metodo = getattr(animale, metodoChiamato)
        
        #nel caso in cui il metodo fosse "dorme" gli passo il parametro del tempo "2 secondi" altrimenti lo chiamo senza parametri
        if (metodo.__name__ != "dorme"):
            metodo()
        else:
            metodo(2)
    time.sleep(1)
