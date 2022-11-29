#Gioco delle regine (N Queen Problem), maggiori informazioni riguardo l'algoritmo qui:
# https://www.geeksforgeeks.org/n-queen-problem-backtracking-3/

#in poche parole l'algoritmo usato è quello di controllare cella per cella se la posizione è valida, in quel caso si posiziona una regina e si 
# procede finché la scacchiera non finisce

global N
N = 8

# Funzione per stampare la scacchiera
def printSolution(board):
    for i in range(N):
        for j in range(N):
            print(board[i][j], end=" ")
        print()


#funzione per controllare se la regina può essere posizionata senza violare le regole del gioco
def isSafe(board, row, col):
    # Controllo riga
    for i in range(col):
        if board[row][i] == 1:
            return False

    # Controllo diagonale 1
    for i, j in zip(range(row, -1, -1),
                    range(col, -1, -1)):
        if board[i][j] == 1:
            return False

    # Controllo diagonale 2
    for i, j in zip(range(row, N, 1),
                    range(col, -1, -1)):
        if board[i][j] == 1:
            return False

    return True


def solveNQf(board, col):

    #Se tutte le regine sono state piazzate torna vero
    if col >= N:
        return True

    # Si esegue la funzione colonna per colonna
    for i in range(N):

        
        if isSafe(board, i, col):

            #Se la cella è valida si posiziona la regina nella cella corrente    
            board[i][col] = 1

            #Si usa la ricorsione per controllare tutte le altre colonne
            if solveNQf(board, col + 1) == True:
                return True

        #Nel caso in cui non ci siano soluzioni per la colonna corrente si piazza uno zero
        board[i][col] = 0
    #Nel caso in cui non siano presenti altre soluzioni si ritorna falso
    return False




def solveNQ():
    board = [[0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0],
             [0, 0, 0, 0, 0, 0, 0, 0]]

    if solveNQf(board, 0) == False:
        print("Nessuna soluzione trovata")
        return False

    printSolution(board)
    return True


solveNQ()
