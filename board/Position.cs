namespace chess_cli.board
{
    class Position
    {
        public int line { get; set; }
        public int column { get; set; }

        public Position(int line, int column)
        {
            this.line = line;
            this.column = column;
        }

        public void setValues(int line, int column)
        {
            this.line = line;
            this.column = column;
        }
    }
}
