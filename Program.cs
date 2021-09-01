using System;
using System.Text;
using chess_cli.board;
using chess_cli.chess;

namespace chess_cli
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Board board = new Board(8, 8);
            board.addPiece(new Rook(board, Color.Preta), new Position(0, 0));
            board.addPiece(new Rook(board, Color.Preta), new Position(1, 3));
            board.addPiece(new King(board, Color.Preta), new Position(2, 4));

            Screen.printBoard(board);
        }
    }
}
