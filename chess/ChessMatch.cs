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
        public bool isCheck { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            isCheck = false;
            vulnerableEnPassant = null;
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

            // Special move castling kingside
            if(piece is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.line, origin.column + 3);
                Position destinyRook = new Position(origin.line, origin.column + 1);
                Piece Rook = board.removePiece(originRook);
                Rook.increaseMovementsNumber();
                board.addPiece(Rook, destinyRook);
            }

            // Special move castling queenside
            if (piece is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.line, origin.column - 4);
                Position destinyRook = new Position(origin.line, origin.column - 1);
                Piece Rook = board.removePiece(originRook);
                Rook.increaseMovementsNumber();
                board.addPiece(Rook, destinyRook);
            }

            // Special move En passant
            if(piece is Pawn)
            {
                if(origin.column != destiny.column && capturedPiece == null)
                {
                    Position pawnPosition;
                    pawnPosition = piece.color == Color.White ? new Position(destiny.line + 1, destiny.column) : new Position(destiny.line - 1, destiny.column);
                    capturedPiece = board.removePiece(pawnPosition);
                    captured.Add(capturedPiece);
                }
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

            // Special move castling kingside
            if (piece is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.line, origin.column + 3);
                Position destinyRook = new Position(origin.line, origin.column + 1);
                Piece Rook = board.removePiece(destinyRook);
                Rook.decreaseMovementsNumber();
                board.addPiece(Rook, originRook);
            }

            // Special move castling queenside
            if (piece is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.line, origin.column - 4);
                Position destinyRook = new Position(origin.line, origin.column - 1);
                Piece Rook = board.removePiece(destinyRook);
                Rook.decreaseMovementsNumber();
                board.addPiece(Rook, originRook);
            }

            // Special move En passant
            if(piece is Pawn)
            {
                if(origin.column != destiny.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(destiny);
                    Position pawnPosition = piece.color == Color.White ? new Position(3, destiny.column) : new Position(4, destiny.column);
                    board.addPiece(pawn, pawnPosition);
                }
            }
        }

        public void play(Position origin, Position destiny)
        {
            Piece capturedPiece = performMovement(origin, destiny);

            if (check(currentPlayer))
            {
                undoMovement(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque");
            }

            isCheck = check(enemyColor(currentPlayer));

            if (isCheckMate(enemyColor(currentPlayer))) finished = true;
            else
            {
                turn++;
                changePlayer();
            }

            Piece piece = board.piece(destiny);
            // Special move En passant
            vulnerableEnPassant = piece is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2) ? piece : null;
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

        public bool check(Color color)
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

        public bool isCheckMate(Color color)
        {
            if (!check(color)) return false;

            foreach(Piece x in gamePieces(color))
            {
                bool[,] matrix = x.possibleMoves();
                for (int i = 0; i < board.lines; i++)
                {
                    for(int j = 0; j < board.columns; j++)
                    {
                        if(matrix[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = performMovement(origin, destiny);
                            bool isCheck = check(color);
                            undoMovement(origin, destiny, capturedPiece);
                            if(!isCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void addNewPiece(char column, int line, Piece piece)
        {
            board.addPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            addNewPiece('a', 1, new Rook(board, Color.White));
            addNewPiece('b', 1, new Knight(board, Color.White));
            addNewPiece('c', 1, new Bishop(board, Color.White));
            addNewPiece('d', 1, new Queen(board, Color.White));
            addNewPiece('e', 1, new King(board, Color.White, this));
            addNewPiece('f', 1, new Bishop(board, Color.White));
            addNewPiece('g', 1, new Knight(board, Color.White));
            addNewPiece('h', 1, new Rook(board, Color.White));
            addNewPiece('a', 2, new Pawn(board, Color.White, this));
            addNewPiece('b', 2, new Pawn(board, Color.White, this));
            addNewPiece('c', 2, new Pawn(board, Color.White, this));
            addNewPiece('d', 2, new Pawn(board, Color.White, this));
            addNewPiece('e', 2, new Pawn(board, Color.White, this));
            addNewPiece('f', 2, new Pawn(board, Color.White, this));
            addNewPiece('g', 2, new Pawn(board, Color.White, this));
            addNewPiece('h', 2, new Pawn(board, Color.White, this));

            addNewPiece('a', 8, new Rook(board, Color.Black));
            addNewPiece('b', 8, new Knight(board, Color.Black));
            addNewPiece('c', 8, new Bishop(board, Color.Black));
            addNewPiece('d', 8, new Queen(board, Color.Black));
            addNewPiece('e', 8, new King(board, Color.Black, this));
            addNewPiece('f', 8, new Bishop(board, Color.Black));
            addNewPiece('g', 8, new Knight(board, Color.Black));
            addNewPiece('h', 8, new Rook(board, Color.Black));
            addNewPiece('a', 7, new Pawn(board, Color.Black, this));
            addNewPiece('b', 7, new Pawn(board, Color.Black, this));
            addNewPiece('c', 7, new Pawn(board, Color.Black, this));
            addNewPiece('d', 7, new Pawn(board, Color.Black, this));
            addNewPiece('e', 7, new Pawn(board, Color.Black, this));
            addNewPiece('f', 7, new Pawn(board, Color.Black, this));
            addNewPiece('g', 7, new Pawn(board, Color.Black, this));
            addNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
