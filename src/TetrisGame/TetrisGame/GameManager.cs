using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//#include "Board.cs"
//#include "Pieces."cs
////#include "IO.h"
////#include <time.h>

namespace TetrisGame
{
    public class GameManager
    {
        private const int BOARD_WIDTH = 10;
        private const int BOARD_HEIGHT = 20;

        // ЧЧЧ —сылки на другие классы ЧЧЧ
        private readonly Pieces pieces;
        private readonly Board board;
        private readonly Random random;

        public int CurrentPiece;
        public int CurrentRotation;
        public int CurrentX;
        public int CurrentY;

        // ЧЧЧ —ледующа€ фигура ЧЧЧ
        private int nextPiece;
        private int nextRotation;

        public GameManager(Board board, Pieces pieces)
        {
            this.pieces = pieces;
            this.board = board;
            random = new Random();

            InitGame();
        }


    }
}