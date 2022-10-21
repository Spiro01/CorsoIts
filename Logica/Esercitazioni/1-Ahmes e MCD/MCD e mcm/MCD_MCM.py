
# MCD con algoritmo di euclide https://www.khanacademy.org/computing/computer-science/cryptography/modarithmetic/a/the-euclidean-algorithm
def MCD(a: int, b: int):

    if (b > a):
        b, a = a, b  # Se b > a scambio le variabili, necessario per il funzionamento dell'algoritmo di euclide

    while (a != 0 and b != 0):
        resto = a % b
        a = b
        b = resto
    return a

#Mcm, per calcolarlo è sufficiente moltiplicare a per b e dividere per l'MCD https://stackoverflow.com/a/3154503
def MCM(a:int,b:int):
    mcd = MCD(a,b)
    return int(a*b / mcd) 