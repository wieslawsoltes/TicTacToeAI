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
