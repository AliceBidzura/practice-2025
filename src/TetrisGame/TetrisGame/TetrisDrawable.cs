using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace TetrisGame
{
    public class TetrisDrawable : IDrawable
    {
        private readonly GameManager game;
        private const int CellSize = 30;

        public TetrisDrawable(GameManager game)
        {
            this.game = game;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Сетка
            canvas.FillColor = Colors.DarkGray;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    canvas.FillRectangle(x * CellSize, y * CellSize, CellSize - 1, CellSize - 1);
                }
            }

            //Заполненные ячейки
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (game.getBoard().mBoard[x, y] == 1) // POS_FILLED
                    {
                        canvas.FillColor = Colors.Gray; // Или GetColorForPiece(...)
                        canvas.FillRectangle(x * CellSize, y * CellSize, CellSize - 1, CellSize - 1);
                    }
                }
            }


            // Фигура
            canvas.FillColor = Colors.Cyan;
            for (int i = 0; i < 5; i++) // x в фигуре
            {
                for (int j = 0; j < 5; j++) // y в фигуре
                {
                    //была ошибка в передаче координат
                    if (game.pieces.GetBlockType(game.CurrentPiece, game.CurrentRotation, j, i) != 0)
                    {
                        int screenX = game.CurrentX + i;
                        int screenY = game.CurrentY + j;

                        canvas.FillRectangle(screenX * CellSize, screenY * CellSize, CellSize - 1, CellSize - 1);
                    }
                }
            }
        }
    }

}

