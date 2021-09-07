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
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] posiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    Console.BackgroundColor = posiblePositions[i, j] ? alteredBackground : originalBackground;
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
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
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;

                Console.ForegroundColor = piece.color == Color.White ? ConsoleColor.White : ConsoleColor.DarkBlue;

                //if (piece.color == Color.White)
                //{
                //    Console.ForegroundColor = ConsoleColor.White;
                //}
                //else
                //{
                //    Console.ForegroundColor = ConsoleColor.DarkBlue;
                //}
                Console.Write(piece);
                Console.ForegroundColor = aux;
                Console.Write(" ");
            }
        }
    }
}
