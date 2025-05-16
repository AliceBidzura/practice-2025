using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace TetrisGame
{
    public class NextPieceDrawable : IDrawable
    {
        private readonly GameManager game;
        private const int CellSize = 25;

        public NextPieceDrawable(GameManager game)
        {
            this.game = game;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.DarkSlateGray;
            canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

            int piece = game.NextPiece;
            int rotation = game.NextRotation;

            canvas.FillColor = Colors.Orange;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game.pieces.GetBlockType(piece, rotation, j, i) != 0)
                    {
                        int x = i * CellSize;
                        int y = j * CellSize;
                        canvas.FillRectangle(x, y, CellSize - 1, CellSize - 1);
                    }
                }
            }
        }
    }
}

