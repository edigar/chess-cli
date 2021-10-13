using chess_cli.board;

namespace chess_cli.chess
{
    class King : Piece
    {
        private ChessMatch chessMatch;

        public King(Board board, Color color, ChessMatch chessMatch) : base(board, color)
        {
            this.chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position position)
        {
            Piece piece = board.piece(position);
            return piece == null || piece.color != this.color;
        }

        private bool castlingTest(Position position)
        {
            Piece piece = board.piece(position);
            return piece != null && piece is Rook && piece.color == color && piece.movementsNumber == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] matrix = new bool[board.lines, board.columns];

            Position position = new Position(0, 0);

            position.setValues(this.position.line - 1, this.position.column);
            if(board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line - 1, this.position.column + 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line, this.position.column + 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line + 1, this.position.column + 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line + 1, this.position.column);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line + 1, this.position.column - 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line, this.position.column - 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            position.setValues(this.position.line - 1, this.position.column - 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            // Special play castling kingside
            if (movementsNumber == 0 && !chessMatch.isCheck)
            {
                Position kingSideRookPosition = new Position(position.line, position.column + 3);
                if (castlingTest(kingSideRookPosition))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if(board.piece(p1) == null && board.piece(p2) == null)
                    {
                        matrix[position.line, position.column + 2] = true;
                    }
                }
            }

            // Special play castling queenside
            if (movementsNumber == 0 && !chessMatch.isCheck)
            {
                Position queenSideRookPosition = new Position(position.line, position.column - 4);
                if (castlingTest(queenSideRookPosition))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        matrix[position.line, position.column - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
