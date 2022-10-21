from os import read

a = int(input("Inserisci il primo termine "))
b = int(input("Inserisci il secondo termine "))

risultato = 0
resto = 0

while a != 1:

    if a % 2 == 0:  # se a è pari esegui a/2 * (2b)
        print(f"{int(a)} e' pari: \t {int(a)}/2 * 2*{b} + {resto} =")

        a = a / 2
        b = b * 2
        risultato = a * b + resto
    else:  # se a è dispari (a-1)b + b
        resto += b
        print(f"{int(a)} e' dispari: \t ({int(a)} - 1) * {b} + {resto} =")
        a = a - 1
        risultato = a * b + resto

print(f"Termine algoritmo: \t {int(a)} * {b} + {resto}")
print(f"Il risultato e' {int(risultato)}")
