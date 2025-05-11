using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Board
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

        public Board(Pieces pieces)
        {
            mPieces = pieces;
            InitBoard();
        }

        public void InitBoard()
        {
            for(int y = 0; y < BOARD_WIDTH; y++)
            {
                for(int x = 0; x < BOARD_HEIGHT; x++)
                {
                    mBoard[y, x] = POS_FREE;
                }
            }
        }

        //InitBoard method is just a nested loop that initializes all the board blocks to POS_FREE.
        public bool IsFreeBlock(int x, int y)
        {
            return (mBoard[x, y] == POS_FREE);
        }

        //StorePiece method, just stores a piece in the board by filling the appropriate blocks as POS_FILLED.
        //просто сохраняет элемент на доске, заполняя соответствующие блоки как POS_FILLED.
        public void StorePiece(int x, int y, int pPiece, int pRotation)
        {
            //PIECE_BLOCKS 5 // Количество горизонтальных и вертикальных блоков фигуры матрицы
            for (int i1 = x, i2 = 0; i1 < x + PIECE_BLOCKS; i1++, i2++)
            {
                for (int j1 = y, j2 = 0; j1 < y + PIECE_BLOCKS; j1++, j2++)
                {
                    if (mPieces.GetBlockType(pPiece, pRotation, j2, i2) != 0)
                    {
                        mBoard[i1, j1] = POS_FILLED;
                    }
                }
            }
        }

    }
}
