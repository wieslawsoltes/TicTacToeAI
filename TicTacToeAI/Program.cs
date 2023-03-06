using System;

TicTacToeAI ai = new TicTacToeAI();
char currentPlayer = 'X';

while (!ai.IsBoardFull() && !ai.CheckForWin('X') && !ai.CheckForWin('O'))
{
    if (currentPlayer == 'X')
    {
        Console.WriteLine("Twoj ruch: ");
        int row = int.Parse(Console.ReadLine());
        int col = int.Parse(Console.ReadLine());
        ai.board[row, col] = 'X';
    }
    else
    {
        int[] bestMove = ai.GetBestMove('O');
        ai.board[bestMove[0], bestMove[1]] = 'O';
        Console.WriteLine("Ruch sztucznej inteligencji: " + bestMove[0] + ", " + bestMove[1]);
    }

    Console.WriteLine(ai.board[0, 0] + " | " + ai.board[0, 1] + " | " + ai.board[0, 2]);
    Console.WriteLine("---------");
    Console.WriteLine(ai.board[1, 0] + " | " + ai.board[1, 1] + " | " + ai.board[1, 2]);
    Console.WriteLine("---------");
    Console.WriteLine(ai.board[2, 0] + " | " + ai.board[2, 1] + " | " + ai.board[2, 2]);

    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
}

if (ai.CheckForWin('X'))
{
    Console.WriteLine("Wygrales!");
}
else if (ai.CheckForWin('O'))
{
    Console.WriteLine("Przegrales!");
}

public class TicTacToeAI
{
    public char[,] board;

    public TicTacToeAI()
    {
        board = new char[3, 3];
        // Inicjalizacja planszy pustymi polami
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }

    // Metoda zwraca true, jeśli plansza jest pełna, w przeciwnym razie false
    public bool IsBoardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Metoda zwraca true, jeśli podany gracz (X lub O) wygrał, w przeciwnym razie false
    public bool CheckForWin(char player)
    {
        // Sprawdzenie wierszy
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
            {
                return true;
            }
        }

        // Sprawdzenie kolumn
        for (int j = 0; j < 3; j++)
        {
            if (board[0, j] == player && board[1, j] == player && board[2, j] == player)
            {
                return true;
            }
        }

        // Sprawdzenie przekątnych
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
        {
            return true;
        }

        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
        {
            return true;
        }

        return false;
    }

    // Metoda wywołująca algorytm minimax dla danej planszy i danego gracza
    public int[] GetBestMove(char player)
    {
        int[] bestMove = new int[2];
        int bestScore = int.MinValue;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = player;
                    int score = MiniMax(player, false);
                    board[i, j] = ' ';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove[0] = i;
                        bestMove[1] = j;
                    }
                }
            }
        }

        return bestMove;
    }

// Implementacja algorytmu minimax
    private int MiniMax(char player, bool isMaximizingPlayer)
    {
        if (CheckForWin('X'))
        {
            return -1;
        }

        if (CheckForWin('O'))
        {
            return 1;
        }

        if (IsBoardFull())
        {
            return 0;
        }

        if (isMaximizingPlayer)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = player;
                        int score = MiniMax(player, false);
                        board[i, j] = ' ';
                        bestScore = Math.Max(score, bestScore);
                    }
                }
            }

            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = player == 'X' ? 'O' : 'X';
                        int score = MiniMax(player, true);
                        board[i, j] = ' ';
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }

            return bestScore;
        }
    }
}