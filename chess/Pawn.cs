using chess_cli.board;

namespace chess_cli.chess
{
    class Pawn : Piece
    {
        private ChessMatch chessMatch;
        public Pawn(Board board, Color color, ChessMatch chessMatch) : base(board, color)
        {
            this.chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool thereIsEnemy(Position position)
        {
            Piece piece = board.piece(position);
            return piece != null || piece.color != this.color;
        }

        private bool free(Position position)
        {
            return board.piece(position) == null;
        }

        //private bool canMove(Position position)
        //{
        //    Piece piece = board.piece(position);
        //    return piece == null || piece.color != this.color;
        //}

        public override bool[,] possibleMoves()
        {
            bool[,] matrix = new bool[board.lines, board.columns];

            Position position = new Position(0, 0);

            if(color == Color.White)
            {
                position.setValues(this.position.line - 1, this.position.column);
                if (board.isValidPosition(position) && free(position))
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line - 2, this.position.column);
                if (board.isValidPosition(position) && free(position) && movementsNumber == 0)
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line - 1, this.position.column - 1);
                if (board.isValidPosition(position) && thereIsEnemy(position))
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line - 1, this.position.column + 1);
                if (board.isValidPosition(position) && thereIsEnemy(position))
                {
                    matrix[position.line, position.column] = true;
                }

                // Special move En passant
                if(position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if(board.isValidPosition(left) && thereIsEnemy(left) && board.piece(left) == chessMatch.vulnerableEnPassant)
                    {
                        matrix[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.isValidPosition(right) && thereIsEnemy(right) && board.piece(right) == chessMatch.vulnerableEnPassant)
                    {
                        matrix[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                position.setValues(this.position.line + 1, this.position.column);
                if (board.isValidPosition(position) && free(position))
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line + 2, this.position.column);
                if (board.isValidPosition(position) && free(position) && movementsNumber == 0)
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line + 1, this.position.column - 1);
                if (board.isValidPosition(position) && thereIsEnemy(position))
                {
                    matrix[position.line, position.column] = true;
                }
                position.setValues(this.position.line + 1, this.position.column + 1);
                if (board.isValidPosition(position) && thereIsEnemy(position))
                {
                    matrix[position.line, position.column] = true;
                }

                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.isValidPosition(left) && thereIsEnemy(left) && board.piece(left) == chessMatch.vulnerableEnPassant)
                    {
                        matrix[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (board.isValidPosition(right) && thereIsEnemy(right) && board.piece(right) == chessMatch.vulnerableEnPassant)
                    {
                        matrix[right.line + 1, right.column] = true;
                    }
                }
            }

            return matrix;
        }
    }
}
