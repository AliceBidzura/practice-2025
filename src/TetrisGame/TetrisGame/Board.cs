using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class Board
    {
        private const int BOARD_WIDTH = 10;
        private const int BOARD_HEIGHT = 20;
        private const int PIECE_BLOCKS = 5;

        private const int POS_FREE = 0;
        private const int POS_FILLED = 1;

        //int mBoard[BOARD_WIDTH][BOARD_HEIGHT]; // Board that contains the pieces
        //Pieces* mPieces;
        public readonly int[,] mBoard = new int[BOARD_WIDTH, BOARD_WIDTH];
        public readonly Pieces mPieces;
    }
}
