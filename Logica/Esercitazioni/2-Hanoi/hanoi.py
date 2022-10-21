
def CalculateHanoiTower(n, from_rod, to_rod, aux_rod, moves):

    if n == 0:
        return
    CalculateHanoiTower(n-1, from_rod, aux_rod, to_rod, moves)

    moves.append(HTMove(n, from_rod, to_rod))
    CalculateHanoiTower(n-1, aux_rod, to_rod, from_rod, moves)


def HanoiTower(n: int):
    moves = []
    htBoard = HTBoard(n)
    htBoard.print()
    CalculateHanoiTower(n, 'A', 'C', 'B', moves)
    for move in moves:
        print(
            f"Move disk {move.disk} from rod {move.from_rod} to rod {move.to_rod}")
        htBoard.exec_move(move)
        htBoard.print()


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
        for i in reversed(range(n)):
            self.A.append(i+1)
    A = []
    B = []
    C = []

    def exec_move(self, move: HTMove):
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
        rowNum = max([len(self.A), len(self.B), len(self.C)])
        print(f"A \t B \t C")
        for i in reversed(range(rowNum)):
            print(
                f"{self.A[i] if len(self.A) > i else ''} \t {self.B[i] if len(self.B) > i else ''} \t {self.C[i] if len(self.C) > i else ''}")
        print()
