
namespace chess_cli.board
{
    class Piece
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
    }
}
