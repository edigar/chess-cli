using chess_cli.board;
using System.Collections.Generic;

namespace chess_cli.chess
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public void performMovement(Position origin, Position destiny)
        {
            Piece piece = board.removePiece(origin);
            piece.increaseMovementsNumber();
            Piece capturedPiece = board.removePiece(destiny);
            board.addPiece(piece, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
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

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in captured)
            {
                if (x.color == color) aux.Add(x);
            }

            return aux;
        }

        public HashSet<Piece> gamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color) aux.Add(x);
            }
            aux.ExceptWith(capturedPieces(color));
            
            return aux;
        }

        public void addNewPiece(char column, int line, Piece piece)
        {
            board.addPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            addNewPiece('a', 8, new Rook(board, Color.Black));
            addNewPiece('h', 8, new Rook(board, Color.Black));
            addNewPiece('e', 8, new King(board, Color.Black));

            addNewPiece('a', 1, new Rook(board, Color.White));
            addNewPiece('h', 1, new Rook(board, Color.White));
            addNewPiece('e', 1, new King(board, Color.White));
        }
    }
}
