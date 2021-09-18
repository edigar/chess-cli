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
        public bool isCheckmate { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            isCheckmate = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece performMovement(Position origin, Position destiny)
        {
            Piece piece = board.removePiece(origin);
            piece.increaseMovementsNumber();
            Piece capturedPiece = board.removePiece(destiny);
            board.addPiece(piece, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void undoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = board.removePiece(destiny);
            piece.decreaseMovementsNumber();
            if(capturedPiece != null)
            {
                board.addPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            board.addPiece(piece, origin);
        }

        public void play(Position origin, Position destiny)
        {
            Piece capturedPiece = performMovement(origin, destiny);

            if (checkmate(currentPlayer))
            {
                undoMovement(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque");
            }

            isCheckmate = checkmate(enemyColor(currentPlayer)) ? true : false;

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
            foreach (Piece x in pieces)
            {
                if (x.color == color) aux.Add(x);
            }
            aux.ExceptWith(capturedPieces(color));
            
            return aux;
        }

        private Color enemyColor(Color color)
        {
            if (color == Color.White) return Color.Black;
            else return Color.White;
        }

        private Piece king(Color color)
        {
            foreach(Piece x in gamePieces(color))
            {
                if(x is King)
                {
                    return x;
                }
            }

            return null;
        }

        public bool checkmate(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("There is no " + color + " king on the board!");
            }

            foreach (Piece x in gamePieces(enemyColor(color)))
            {
                bool[,] matrix = x.possibleMoves();
                if(matrix[K.position.line, K.position.column])
                {
                    return true;
                }
            }

            return false;
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
