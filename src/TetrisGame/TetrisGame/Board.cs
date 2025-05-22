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
        public readonly int[,] mBoard = new int[BOARD_WIDTH, BOARD_HEIGHT];
        public readonly Pieces mPieces;

        //public int GetBlock(int x, int y)
        //{
        //    return mBoard[x, y];
        //}

        public Board(Pieces pieces)
        {
            mPieces = pieces;
            InitBoard();
        }

        public void InitBoard()
        {
            for (int y = 0; y < BOARD_HEIGHT; y++)
            {
                for (int x = 0; x < BOARD_WIDTH; x++)
                {
                    mBoard[x, y] = POS_FREE;
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
                    //проверка выхода за границы поля
                    if (i1 >= 0 && i1 < BOARD_WIDTH && j1 >= 0 && j1 < BOARD_HEIGHT)
                    {
                        if (mPieces.GetBlockType(pPiece, pRotation, j2, i2) != 0)
                        {
                            mBoard[i1, j1] = POS_FILLED;
                        }
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                if (mBoard[x, 0] == POS_FILLED)
                {
                    return true;
                }
            }
            return false;
        }

        //Y: Vertical position in blocks of the line to \
        public void DeleteLine(int y)
        {
            for (int j = y; j > 0; j--) // rows
            {
                for (int i = 0; i < BOARD_WIDTH; i++) //cols по X
                {
                    mBoard[i, j] = mBoard[i, j - 1];
                }
            }

            // верхняя строка — чистая
            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                mBoard[i, 0] = POS_FREE;
            }
        }

        public int DeletePossibleLines()
        {
            int linesRemoved = 0;

            for (int y = 0; y < BOARD_HEIGHT; y++)
            {
                int filled = 0;
                //Фатальный баг — бесконечный цикл
                for (int x = 0; x < BOARD_WIDTH; x++)
                {
                    if (mBoard[x, y] == POS_FILLED)
                    {
                        filled++;
                    }
                }
                if (filled == BOARD_WIDTH)
                {
                    DeleteLine(y);
                    linesRemoved++;
                }

            }

            return linesRemoved;
        }

        public bool IsPossibleToMovement(int x, int y, int pPiece, int pRotation)
        {
            for (int i1 = x, i2 = 0; i1 < x + PIECE_BLOCKS; i1++, i2++)
            {
                for (int j1 = y, j2 = 0; j1 < y + PIECE_BLOCKS; j1++, j2++)
                {
                    // Вне поля
                    if (i1 < 0 || i1 >= BOARD_WIDTH || j1 >= BOARD_HEIGHT)
                    {
                        if (mPieces.GetBlockType(pPiece, pRotation, j2, i2) != 0)
                            return false;
                    }

                    // Столкновение
                    if (j1 >= 0 && mPieces.GetBlockType(pPiece, pRotation, j2, i2) != 0 &&
                        !IsFreeBlock(i1, j1))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
