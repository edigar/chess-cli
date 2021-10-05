using chess_cli.board;

namespace chess_cli.chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
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

            position.setValues(this.position.line - 1, this.position.column);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.line = position.line - 1;
            }

            position.setValues(this.position.line + 1, this.position.column);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.line = position.line + 1;
            }

            position.setValues(this.position.line, this.position.column + 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.column = position.column + 1;
            }

            position.setValues(this.position.line, this.position.column - 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.column = position.column - 1;
            }

            position.setValues(this.position.line - 1, this.position.column - 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.setValues(position.line - 1, position.column - 1);
            }

            position.setValues(this.position.line - 1, this.position.column + 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.setValues(position.line - 1, position.column + 1);
            }

            position.setValues(this.position.line + 1, this.position.column + 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.setValues(position.line + 1, position.column + 1);
            }

            position.setValues(this.position.line + 1, this.position.column - 1);
            while (board.isValidPosition(position) && canMove(position))
            {
                matrix[position.line, position.column] = true;
                if (board.piece(position) != null && board.piece(position).color != color)
                {
                    break;
                }
                position.setValues(position.line + 1, position.column - 1);
            }

            return matrix;
        }
    }
}
