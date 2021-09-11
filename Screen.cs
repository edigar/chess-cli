using chess_cli.board;
using chess_cli.chess;
using System;
using System.Collections.Generic;
namespace chess_cli
{
    class Screen
    {
        public static void printMatch(ChessMatch chessMatch)
        {
            printBoard(chessMatch.board);
            Console.WriteLine();
            printCapturedPieces(chessMatch);
            Console.WriteLine("Turno: " + chessMatch.turn);
            Console.WriteLine("Aguardando jogada: " + chessMatch.currentPlayer);
        }

        public static void printCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Peças capturadas");
            Console.Write("Brancas: ");
            printGroup(chessMatch.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            printGroup(chessMatch.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach(Piece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

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
                Console.Write(piece);
                Console.ForegroundColor = aux;
                Console.Write(" ");
            }
        }
    }
}
