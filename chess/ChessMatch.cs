using chess_cli.board;
using System;

namespace chess_cli.chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
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

        public void play(Position origin, Position destiny)
        {
            performMovement(origin, destiny);
            turn++;
            changePlayer();
        }

        public void validateOrigin(Position position)
        {
            if(board.piece(position) == null)
                throw new BoardException("Não existe peça na posição de origem escolhida");

            if(currentPlayer != board.piece(position).color)
                throw new BoardException("Peça de origem escolhida não é sua");

            if (!board.piece(position).thereIsPossibleMoves())
                throw new BoardException("Não há movimentos possíveis para a peça escolhida");
        }

        public void validateDestiny(Position origin, Position destiny)
        {
            if (!board.piece(origin).canMoveTo(destiny))
                throw new BoardException("Posição de destino inválida");
        }

        public void changePlayer()
        {
            currentPlayer = currentPlayer == Color.White ? Color.Black : Color.White;
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
