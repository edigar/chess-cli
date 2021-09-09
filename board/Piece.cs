
namespace chess_cli.board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementsNumber { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.movementsNumber = 0;
        }

        public void increaseMovementsNumber()
        {
            movementsNumber++;
        }

        public bool thereIsPossibleMoves()
        {
            bool[,] matrix = possibleMoves();
            for(int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (matrix[i, j]) return true;
                }
            }

            return false;
        }

        public bool canMoveTo(Position position)
        {
            return possibleMoves()[position.line, position.column];
        }

        public abstract bool[,] possibleMoves();
    }
}
