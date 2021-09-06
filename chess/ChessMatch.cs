using chess_cli.board;
using System;

namespace chess_cli.chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void performMovement(Position origin, Position destiny)
        {
            Piece piece = board.removePiece(origin);
            piece.increaseMovementsNumber();
            Piece capturedPiece = board.removePiece(destiny);
            board.addPiece(piece, destiny);
        }

        private void putPieces()
        {
            board.addPiece(new Rook(board, Color.Black), new ChessPosition('a', 8).toPosition());
            board.addPiece(new Rook(board, Color.Black), new ChessPosition('h', 8).toPosition());
            board.addPiece(new King(board, Color.Black), new ChessPosition('e', 8).toPosition());

            board.addPiece(new Rook(board, Color.White), new ChessPosition('a', 1).toPosition());
            board.addPiece(new Rook(board, Color.White), new ChessPosition('h', 1).toPosition());
            board.addPiece(new King(board, Color.White), new ChessPosition('e', 1).toPosition());
        }
    }
}
