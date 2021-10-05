using chess_cli.board;

namespace chess_cli.chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "H";
        }

        private bool canMove(Position position)
        {
            Piece piece = board.piece(position);
            return piece == null || piece.color != this.color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] matrix = new bool[board.lines, board.columns];

            Position position = new Position(0, 0);

            position.setValues(this.position.line - 1, this.position.column - 2);
            if(board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line - 2, this.position.column - 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line - 2, this.position.column + 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line - 1, this.position.column + 2);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line + 1, this.position.column + 2);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line + 2, this.position.column + 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line + 2, this.position.column - 1);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }
            position.setValues(this.position.line + 1, this.position.column - 2);
            if (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
            }

            return matrix;
        }
    }
}
