using chess_cli.board;

namespace chess_cli.chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
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
            }

            return matrix;
        }
    }
}
