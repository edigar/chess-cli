using System;

namespace chess_cli.board
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message)
        {

        }
    }
}
