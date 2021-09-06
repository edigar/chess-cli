using chess_cli.board;
using chess_cli.chess;
using System;

namespace chess_cli
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if(board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readChessPosition()
        {
            string userPlay = Console.ReadLine();
            char column = userPlay[0];
            int line = int.Parse(userPlay[1] + "");

            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece)
        {
            ConsoleColor aux = Console.ForegroundColor;
            if (piece.color == Color.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
            } 
            else 
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            Console.Write(piece);
            Console.ForegroundColor = aux;
        }
    }
}
