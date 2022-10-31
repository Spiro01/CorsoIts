
import string


def CalculateHanoiTower(n: int, from_rod: string, to_rod: string, aux_rod: string, moves):
    """Algoritmo torre di Hanoi

    Args:
        n (int): numero di dischi
        from_rod (string): nome asta iniziale
        to_rod (string): nome asta finale
        aux_rod (string): nome asta finale
        moves (HTMove[]): Lista di mosse eseguite
    """
    if n == 0:
        return
    
    #Uso la ricorsione per calcolare le mosse necessarie alla risoluzione della torre di hanoi, 
    # per maggiori info vedi file Optimal_Algorithms_for_Tower_of_Hanoi_Problems_wit.pdf pagina 40 sezione 3
    CalculateHanoiTower(n-1, from_rod, aux_rod, to_rod, moves)
    #La mossa appena eseguita è aggiunta alla lista
    moves.append(HTMove(n, from_rod, to_rod))
    
    CalculateHanoiTower(n-1, aux_rod, to_rod, from_rod, moves)


def HanoiTower(n: int):
    """Calcola torre di hanoi e stampa graficamente il risultato

    Args:
        n (int): numero di dischi
    """
    moves = []
    htBoard = HTBoard(n)
    htBoard.print()
    CalculateHanoiTower(n, 'A', 'C', 'B', moves)
    for move in moves:
        print(
            f"Sorgente: {ord(move.from_rod)-64}a pila; Destinazione: {ord(move.to_rod)-64}a pila")
        print(f"L'elemento che sposto e' il disco {move.disk}")
        htBoard.exec_move(move)
        htBoard.print()
    print(f"Il processo termina in {len(moves)} passi")


class HTMove:
    def __init__(self, disk, from_rod, to_rod):
        self.disk = disk
        self.from_rod = from_rod
        self.to_rod = to_rod
    disk = ''
    from_rod = ''
    to_rod = ''


class HTBoard:
    def __init__(self, n: int):
        #Riempi la prima colonna con n dischi
        for i in reversed(range(n)):
            self.A.append(i+1)
    A = []
    B = []
    C = []

    def exec_move(self, move: HTMove):
        """Esegui mossa

        Args:
            move (HTMove): Mossa da eseguire
        """
        if (move.from_rod == 'A'):
            self.A.remove(move.disk)
            if (move.to_rod == 'B'):
                self.B.append(move.disk)
            elif (move.to_rod == 'C'):
                self.C.append(move.disk)

        elif (move.from_rod == 'B'):
            self.B.remove(move.disk)
            if (move.to_rod == 'A'):
                self.A.append(move.disk)
            elif (move.to_rod == 'C'):
                self.C.append(move.disk)

        elif (move.from_rod == 'C'):
            self.C.remove(move.disk)
            if (move.to_rod == 'A'):
                self.A.append(move.disk)
            elif (move.to_rod == 'B'):
                self.B.append(move.disk)

    def print(self):
        #Cerca la colonna con più elementi
        rowNum = max([len(self.A), len(self.B), len(self.C)])
        print(f"A \t B \t C")
        print("-------------------")
        for i in reversed(range(rowNum)):
            print(
                f"{self.A[i] if len(self.A) > i else ''} \t {self.B[i] if len(self.B) > i else ''} \t {self.C[i] if len(self.C) > i else ''}")
        print()
