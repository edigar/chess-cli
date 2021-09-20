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
            try
            {
                //Console.OutputEncoding = Encoding.UTF8;
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.readChessPosition().toPosition();
                        match.validateOrigin(origin);

                        bool[,] possiblePositions = match.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(match.board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.readChessPosition().toPosition();
                        match.validateDestiny(origin, destiny);

                        match.play(origin, destiny);
                    } catch(BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.printMatch(match);
            } catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
